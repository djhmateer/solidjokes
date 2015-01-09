using System;
using System.IO;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;
using System.Collections.Generic;
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
                } else {
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

        public ActionResult Spotify() {
            return View();
        }

        public ActionResult SpotifyArtistSearch(){
            return View();
        }

        [HttpPost]
        public ActionResult SpotifyArtistSearch(string artistName) {
            string json = CallSpotifyAPI(artistName);

            var jsonNoArtistsRootElement = JObject.Parse(json)["artists"].ToString();
            ArtistsResponse result = JsonConvert.DeserializeObject<ArtistsResponse>(jsonNoArtistsRootElement);

            return View(result);
        }

        public ActionResult SpotifyAuthTestLogin() {
            return View();
        }

        private static string CallSpotifyAPI(string artistName) {
            if (!String.IsNullOrWhiteSpace(artistName)) artistName = HttpUtility.UrlEncode(artistName);

            var url = String.Format("https://api.spotify.com/v1/search?q={0}&type=artist", artistName);

            string text = null;
            bool done = false;
            while (!done) {
                try {
                    Console.WriteLine("Requesting: " + url);

                    var request = (HttpWebRequest)HttpWebRequest.Create(url);
                    request.Accept = "application/json";

                    var response = (HttpWebResponse)request.GetResponse();

                    using (var sr = new StreamReader(response.GetResponseStream())) {
                        text = sr.ReadToEnd();
                    }

                    done = true;
                }
                catch (WebException ex) {
                    Console.WriteLine("Exception: " + ex.Message);
                    Console.WriteLine("Retrying in 1 second...");
                    System.Threading.Thread.Sleep(1000);
                }
            }

            if (String.IsNullOrEmpty(text)) throw new InvalidOperationException();
            return text;
        }
    }


    public class ArtistsResponse {
        public string Href { get; set; }
        public int Total { get; set; }

        public List<Artist> Items { get; set; }

        public class Artist {
            public string Id { get; set; }
            public List<SpotifyImage> Images { get; set; }
            public string Name { get; set; }

            public class SpotifyImage {
                public int Height { get; set; }
                public string Url { get; set; }
                public int Width { get; set; }
            }
        }
    }
}