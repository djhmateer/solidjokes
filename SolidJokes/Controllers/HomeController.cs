using System.Collections.Generic;
using System.Web.Mvc;
using Core.Models;
using Core.Services;

namespace SolidJokes.Controllers {
    public class HomeController : Controller, IHomeController {
        public ActionResult Index() {
            var viewer = new StoryViewer();
            List<Story> stories = null;
            stories = viewer.ShowAllStoriesHighestRatingFirst();
            return View(stories);

        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}