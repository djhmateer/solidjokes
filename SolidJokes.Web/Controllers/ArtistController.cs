using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SolidJokes.Web.Controllers
{
    public class ArtistController : Controller
    {
        // GET: Artist/Details/12345
        public ActionResult Details(string id){
            var spotifyHelper = new SpotifyHelper();
            var stopWatchResult = new StopWatchResult();
            string json = spotifyHelper.CallSpotifyAPIArtist(stopWatchResult: stopWatchResult,
                artistCode: id);
            ViewBag.APITime = stopWatchResult.TimeInMs;
            ViewBag.Id = id;
            var result = JsonConvert.DeserializeObject<ArtistDetails>(json);

            return View(result);
        }
    }

    public class ArtistDetails{
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