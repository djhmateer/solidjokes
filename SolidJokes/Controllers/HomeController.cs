using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace SolidJokes.Controllers {
    public class HomeController : Controller {
        private readonly ILogger logger;

        public HomeController(ILogger logger) {
            this.logger = logger;
        }

        public ActionResult Index() {
            logger.Log("In Index of Home Controller");
            return View();
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

    public interface ILogger { void Log(string text);}
    public class Logger : ILogger {
        public void Log(string text) {
            Console.WriteLine(text);
            Debug.WriteLine(text);
        }
    }
}