using System.Web.Mvc;

namespace Aviato.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult PageNotFound()
        {
            return View();
        }

        public ActionResult BadRequest()
        {
            return View();
        }

        //internal server error
        public ActionResult ISE()
        {
            return View();
        }
    }
}