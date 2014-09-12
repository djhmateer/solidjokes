using System.Collections.Generic;
using System.Web.Mvc;
using Core.Models;
using Core.Services;

namespace SolidJokes.Controllers {
    public class HomeController : Controller, IHomeController {
        public ActionResult Index() {
            var viewer = new JokeViewer();
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