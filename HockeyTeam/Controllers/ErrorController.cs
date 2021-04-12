using System.Web.Mvc;

namespace HockeyTeam.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult FileUploadLimitExceeded()
        {
            return View();
        }
    }
}