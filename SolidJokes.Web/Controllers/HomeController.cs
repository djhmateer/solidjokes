﻿using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SolidJokes.Web.Controllers {
    public class HomeController : Controller, IHomeController {
        private readonly IJokeViewer viewer;
        private readonly IJokeVoter voter;

        public HomeController(IJokeViewer viewer, IJokeVoter voter) {
            this.viewer = viewer;
            this.voter = voter;
        }

        public ActionResult Index(string sortOrder, string message) {
            if (sortOrder == null) sortOrder = "ratingDescending";
            List<Joke> jokes = null;
            switch (sortOrder) {
                case "ratingDescending":
                    jokes = viewer.ShowAllJokesHighestRatingFirst();
                    break;
                case "dateCreatedDescending":
                    jokes = viewer.ShowAllJokesByDateCreatedDescending();
                    break;
            }
            DisplayMessageToUserIfRequired(message);
            return View(jokes);
        }

        private void DisplayMessageToUserIfRequired(string message) {
            // Is there a message, and what type to display to user ie green or red
            if (message != null) {
                if (message.Contains("Thank you for voting!") && message != "") {
                    ViewBag.GreenMessage = true;
                }
                else {
                    ViewBag.RedMessage = true;
                }
                ViewBag.Message = message;
            }
        }

        public ActionResult Vote(int? storyID, string sortOrder = "ratingDescending") {
            JokeVoterResult result = voter.AddVote(storyID);
            // Display success or fail message of voting
            return RedirectToAction("Index", "Home", new { sortOrder = sortOrder, message = result.Message });
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult SpotifyArtistSearch(string artist = "", int offset = 0) {
            // First call with no parameters
            if (artist == "") {
                ViewBag.InitialArtist = "muse";
                return View();
            }

            var spotifyHelper = new SpotifyHelper();
            var stopWatchResult = new StopWatchResult();
            string json = spotifyHelper.CallSpotifyAPISearch(artist, offset, stopWatchResult);
            ViewBag.APITime = stopWatchResult.TimeInMs;

            var jsonNoArtistsRootElement = JObject.Parse(json)["artists"].ToString();
            ArtistsResponse result = JsonConvert.DeserializeObject<ArtistsResponse>(jsonNoArtistsRootElement);

            ViewBag.ArtistSearchedFor = artist;

            ViewBag.ShowPrevious = false;
            if (offset >= 50) {
                ViewBag.OffsetPrevious = offset - 50;
                ViewBag.ShowPrevious = true;
            }

            ViewBag.ShowNext = false;
            if (offset + 50 < result.Total) {
                ViewBag.OffsetNext = offset + 50;
                ViewBag.ShowNext = true;
            }

            return View(result);
        }

        public ActionResult Spotify() {
            return View();
        }
    }

    public class StopWatchResult {
        public string TimeInMs { get; set; }
    }
    public class SpotifyHelper {
        public string CallSpotifyAPIArtist(string artistCode, StopWatchResult stopWatchResult){
            string url;
            string json;
            if (artistCode == "Random"){
                // %3A is :
                url = "https://api.spotify.com/v1/search?q=year:0000-9999&type=artist&market=GB";
                json = CallAPI(stopWatchResult, url);
                var jsonNoArtistsRootElement = JObject.Parse(json)["artists"].ToString();
                var result = JsonConvert.DeserializeObject<ArtistsResponse>(jsonNoArtistsRootElement);

                var total = result.Total;
                var random = new Random();
                //int offset = random.Next(1, total);
                int offset = random.Next(1, 100000);


                //url = "https://api.spotify.com/v1/search?q=year%3A2001&type=artist&market=US&limit=1&offset=12345";
                url = "https://api.spotify.com/v1/search?q=year:0000-9999&type=artist&market=GB&limit=1&offset=" + offset;
                json = CallAPI(stopWatchResult, url);
                jsonNoArtistsRootElement = JObject.Parse(json)["artists"].ToString();
                result = JsonConvert.DeserializeObject<ArtistsResponse>(jsonNoArtistsRootElement);

                artistCode = result.Items[0].Id;
                // now we want to get the detail!
            }
            url = String.Format("https://api.spotify.com/v1/artists/{0}", artistCode);
            json = CallAPI(stopWatchResult, url);
            return json;
        }
        public string CallSpotifyAPISearch(string artistName, int offset, StopWatchResult stopWatchResult) {
            if (!String.IsNullOrWhiteSpace(artistName)) artistName = HttpUtility.UrlEncode(artistName);
            var url = String.Format("https://api.spotify.com/v1/search?q={0}&offset={1}&limit=50&type=artist", artistName, offset);
            var text = CallAPI(stopWatchResult, url);
            return text;
        }

        private static string CallAPI(StopWatchResult stopWatchResult, string url) {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            string text = null;
            bool done = false;
            while (!done) {
                try {
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Accept = "application/json";

                    var response = (HttpWebResponse)request.GetResponse();

                    using (var sr = new StreamReader(response.GetResponseStream())) {
                        text = sr.ReadToEnd();
                    }

                    done = true;
                }
                catch (WebException ex) {
                    Debug.WriteLine("Exception: " + ex.Message);
                    Thread.Sleep(100);
                }
            }

            if (String.IsNullOrEmpty(text)) throw new InvalidOperationException();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:0}", ts.TotalMilliseconds);
            stopWatchResult.TimeInMs = elapsedTime;
            return text;
        }
    }

    public class ArtistsResponse {
        // Overall Href of the query
        public string Href { get; set; }
        public List<Artist> Items { get; set; }
        public string Next { get; set; }
        public int Offset { get; set; }
        public string Previous { get; set; }
        public int Total { get; set; }

        public class Artist {
            public SpotifyURL External_urls { get; set; }
            public class SpotifyURL {
                public string Spotify { get; set; }
            }

            // blank very often
            //public List<Genre> Genres { get; set; }

            // API call for details of the artistName
            public string Href { get; set; }
            public string Id { get; set; }
            public List<SpotifyImage> Images { get; set; }
            public string Name { get; set; }
            public int Popularity { get; set; }
            public string Type { get; set; }
            public string Uri { get; set; }

            public class SpotifyImage {
                public int Height { get; set; }
                public string Url { get; set; }
                public int Width { get; set; }
            }
        }
    }
}