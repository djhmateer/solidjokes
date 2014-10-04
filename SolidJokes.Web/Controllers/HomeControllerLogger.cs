using System.Diagnostics;
using System.Web.Mvc;

namespace SolidJokes.Web.Controllers {
    public class HomeControllerLogger : Controller, IHomeController {
        private readonly IHomeController homeController;

        public HomeControllerLogger(IHomeController homeController) {
            this.homeController = homeController;
        }

        //public ActionResult Index(string sortOrder = "ratingDescending", string message = "") {
        public ActionResult Index(string sortOrder, string message) {
            Debug.WriteLine("In Index of HomeControllerLogger");
            var result = homeController.Index(sortOrder, message);
            Debug.WriteLine("End Index of HomeControllerLogger");
            return result;
        }

        public ActionResult About() {
            Debug.WriteLine("In About of HomeControllerLogger");
            var result = homeController.About();
            Debug.WriteLine("End About of HomeControllerLogger");
            return result;
        }
    }
}