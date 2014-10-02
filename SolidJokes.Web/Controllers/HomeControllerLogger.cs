using SolidJokes.Controllers;
using System.Diagnostics;
using System.Web.Mvc;

namespace SolidJokes.Web.Controllers {
    public class HomeControllerLogger : Controller, IHomeController {
        private readonly IHomeController homeController;

        public HomeControllerLogger(IHomeController homeController) {
            this.homeController = homeController;
        }

        public ActionResult Index() {
            Debug.WriteLine("In Index of HomeControllerLogger");
            var result = homeController.Index();
            Debug.WriteLine("End Index of HomeControllerLogger");
            return result;
        }

        public ActionResult About() {
            Debug.WriteLine("In About of HomeControllerLogger");
            var result = homeController.About();
            Debug.WriteLine("End About of HomeControllerLogger");
            return result;
        }

        public ActionResult Contact() {
            return homeController.Contact();
        }
    }
}