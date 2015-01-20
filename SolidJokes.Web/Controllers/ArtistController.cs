using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SolidJokes.Web.Controllers {
    public class ArtistController : Controller {
        public ActionResult Details(string id){
            var spotifyHelper = new SpotifyHelper();
            var stopWatchResult = new StopWatchResult();
            string json = spotifyHelper.CallSpotifyAPIArtist(stopWatchResult: stopWatchResult,
                artistCode: id);
            ViewBag.Id = id;
            var artistDetails = JsonConvert.DeserializeObject<ArtistDetails>(json);

            var apiDebugList = new List<APIDebug>();
            var apiDebug = new APIDebug{
                APITime = String.Format("{0:0}", stopWatchResult.ElapsedTime.TotalMilliseconds),
                APIURL = artistDetails.Href
            };
            apiDebugList.Add(apiDebug);

            var apiResult = spotifyHelper.CallSpotifyAPIArtistTopTracks(stopWatchResult, id);
            var artistTopTracks = JsonConvert.DeserializeObject<ArtistTopTracks>(apiResult.Json);
            apiDebug = new APIDebug {
                APITime = String.Format("{0:0}", stopWatchResult.ElapsedTime.TotalMilliseconds),
                APIURL = apiResult.Url
            };
            apiDebugList.Add(apiDebug);

            // Only want top 5 tracks in toptracks
            var tracks = artistTopTracks.tracks;
            var top5 = tracks.OrderByDescending(x => x.popularity).Take(5);
            artistTopTracks.tracks = top5.ToList();
            var vm = new ArtistDetailsViewModel{
                ArtistDetails = artistDetails,
                ArtistTopTracks = artistTopTracks,
                APIDebugList = apiDebugList
            };
            return View(vm);
        }
    }

    public class APIDebug {
        public string APITime { get; set; }
        public string APIURL { get; set; }
    }

    public class ArtistDetailsViewModel {
        public ArtistDetails ArtistDetails { get; set; }
        public ArtistTopTracks ArtistTopTracks { get; set; }
        public List<APIDebug> APIDebugList { get; set; } 
    }

    public class ArtistTopTracks {
        public List<Track> tracks { get; set; }
        public class Track {
            public Album album { get; set; }
            public List<Artist> artists { get; set; }
            public List<string> available_markets { get; set; }
            public int disc_number { get; set; }
            public int duration_ms { get; set; }
            public bool @explicit { get; set; }
            public ExternalIds external_ids { get; set; }
            public ExternalUrls3 external_urls { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public int popularity { get; set; }
            public string preview_url { get; set; }
            public int track_number { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
        }
        public class ExternalUrls {
            public string spotify { get; set; }
        }

        public class Image {
            public int height { get; set; }
            public string url { get; set; }
            public int width { get; set; }
        }

        public class Album {
            public string album_type { get; set; }
            public List<string> available_markets { get; set; }
            public ExternalUrls external_urls { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public List<Image> images { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
        }

        public class ExternalUrls2 {
            public string spotify { get; set; }
        }

        public class Artist {
            public ExternalUrls2 external_urls { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string uri { get; set; }
        }

        public class ExternalIds {
            public string isrc { get; set; }
        }

        public class ExternalUrls3 {
            public string spotify { get; set; }
        }
    }

    public class ArtistDetails {
        public class ExternalUrls {
            public string Spotify { get; set; }
        }
        public ExternalUrls external_urls { get; set; }

        public class Followers {
            public object href { get; set; }
            public int Total { get; set; }
        }
        public Followers followers { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public class Image {
            public int Height { get; set; }
            public string URL { get; set; }
            public int Width { get; set; }
        }
        public List<Image> Images { get; set; }

        public string Name { get; set; }
        public int Popularity { get; set; }
        public string Type { get; set; }
        public string Uri { get; set; }
    }
}