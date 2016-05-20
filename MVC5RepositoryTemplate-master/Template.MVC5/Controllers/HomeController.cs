using System.Web.Mvc;

namespace Template.MVC5.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult ServiceRep()
        {
            return View();
        }
    }
}
