using System.Diagnostics;
using System.Web.Mvc;
using SolidJokes.Controllers;

namespace SolidJokes
{
    public class HomeControllerLogger : Controller, IHomeController {
        private readonly IHomeController homeController;

        public HomeControllerLogger(IHomeController homeController) {
            this.homeController = homeController;
        }

        public ActionResult Index()
        {
            Debug.WriteLine("In Index of HomeControllerLogger");
            var result = homeController.Index();
            Debug.WriteLine("End Index of HomeControllerLogger");
            return result;
        }

        public ActionResult About()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult Contact()
        {
            throw new System.NotImplementedException();
        }
    }
}