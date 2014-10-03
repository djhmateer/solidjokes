using System.Web.Mvc;

namespace SolidJokes.Web.Controllers
{
    public interface IHomeController
    {
        ActionResult Index();
        ActionResult About();
        ActionResult Contact();
    }
}