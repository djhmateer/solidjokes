using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SolidJokes.Web.Controllers
{
    public class ArtistController : Controller
    {
        // GET: Artist/Details/12345
        public ActionResult Details(string id){
            var d = new ArtistDetails{Id = id};
            //var spotifyHelper = new SpotifyHelper();
            //var stopWatchResult = new StopWatchResult();
            //string json = spotifyHelper.CallSpotifyAPI(artist, offset, stopWatchResult);
            //ViewBag.APITime = stopWatchResult.TimeInMs;

            //var jsonNoArtistsRootElement = JObject.Parse(json)["artists"].ToString();
            //ArtistsResponse result = JsonConvert.DeserializeObject<ArtistsResponse>(jsonNoArtistsRootElement);

            return View(d);
        }
    }

    public class ArtistDetails{
        public string Id { get; set; }
    }
}