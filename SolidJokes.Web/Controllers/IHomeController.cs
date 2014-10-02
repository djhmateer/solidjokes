using System.Web.Mvc;

namespace SolidJokes.Controllers
{
    public interface IHomeController
    {
        ActionResult Index();
        ActionResult About();
        ActionResult Contact();
    }
}