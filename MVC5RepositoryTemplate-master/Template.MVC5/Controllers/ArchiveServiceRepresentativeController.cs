using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Template.BusinessLogic.Implementation;

namespace Template.MVC5.Controllers
{
    public class ArchiveServiceRepresentativeController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        readonly ServiceRepresentativeBusiness servrep = new ServiceRepresentativeBusiness();
        readonly ArchiveServiceRepresentativeBusiness arservrep = new ArchiveServiceRepresentativeBusiness();

        public ActionResult GetAll()
        {
            ViewBag.Count = arservrep.GetAllArchServReps().Count;
            return View(arservrep.GetAllArchServReps());
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            return View(arservrep.GetArchServRep(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //ArchiveServiceRepresentativeView model, 
        public ActionResult DeleteConfirmed(string id)
        {
            arservrep.DeleteArchive(id);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult RestoreServRep(string id)
        {
            return View(arservrep.GetArchServRep(id));
        }

        [HttpPost, ActionName("RestoreServRep")]
        [ValidateAntiForgeryToken]
        public ActionResult Restore(string id)
        {
            arservrep.Restore(id);           
            return RedirectToAction("GetAll", "ServiceRepresentative");
        }

        public ActionResult SearchServRep()
        {
            var _i = new ArchiveServiceRepresentativeBusiness();
            return View(_i.GetAllArchServReps());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchServRep(string ServRepId)
        {
            var _i = new ArchiveServiceRepresentativeBusiness();
            var search = from c in _i.GetAllArchServReps() select c;
            if (!search.Equals(null))
            {
                search = search.Where(d => d.ServiceRepIdNo.Equals(ServRepId));
            }
          
            else 
            {
                ViewBag.Error = "Service representative not found";
                return View();
            }

            return View(search);
        }
    }
}
