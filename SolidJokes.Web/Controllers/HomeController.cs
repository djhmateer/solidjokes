using System.Collections.Generic;
using System.Web.Mvc;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;

namespace SolidJokes.Web.Controllers {
    public class HomeController : Controller, IHomeController {
        private readonly IJokeViewer viewer;
        public HomeController(IJokeViewer viewer) {
            this.viewer = viewer;
        }

        public ActionResult Index() {
            List<Joke> jokes = viewer.ShowAllJokesHighestRatingFirst();
            return View(jokes);
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