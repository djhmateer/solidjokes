using System.Web.Mvc;

namespace SolidJokes.Web.Controllers
{
    public interface IHomeController
    {
        ActionResult Index(string sortOrder = "ratingDescending", string message = "");
        ActionResult About();
    }
}