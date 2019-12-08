using System.Web.Mvc;

namespace PonyWebChallenge.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
        }
    }
}