using System.Linq;
using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class ServiceRepresentativeController : Controller
    {
        // GET: ServiceRepresentative

        readonly ServiceRepresentativeBusiness servrep = new ServiceRepresentativeBusiness();
        readonly ArchiveServiceRepresentativeBusiness arservrep = new ArchiveServiceRepresentativeBusiness();
        public ActionResult GetAll()
        {
            ViewBag.Count = servrep.GetAll().Count;
            return View(servrep.GetAll());
        }

        public ActionResult SearchServRep()
        {
            var _i = new ServiceRepresentativeBusiness();
            return View(_i.GetAll());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchServRep(string ServRepId)
        {
            var _i = new ServiceRepresentativeBusiness();
            var search = from c in _i.GetAll() select c;
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
        public ActionResult AddServiceRep()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddServiceRep(ServiceRepresentativeView model)
        {
            if (ModelState.IsValid)
            {
                foreach (ServiceRepresentativeView v in servrep.GetAll())
                {
                    if (model.IDNumber == v.IDNumber)
                    {
                        ViewBag.Result = "Service representative with this ID Number already exist.";
                        return View(model);
                    }
                }
                servrep.AddServiceRep(model);
                return RedirectToAction("GetAll");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditServiceRep(string id)
        {
            return View(servrep.GetbyServRepNo(id));
        }

        [HttpPost, ActionName("EditServiceRep")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceRepresentativeView model)
        {
            if (ModelState.IsValid)
            {
                servrep.UpdateServiceRep(model);
                return RedirectToAction("GetAll");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Archive(string id)
        {
            return View(servrep.GetbyServRepNo(id));
        }

        [HttpPost, ActionName("Archive")]
        [ValidateAntiForgeryToken]
        public ActionResult ArchiveConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                arservrep.ArchiveServiceRep(id);
                return RedirectToAction("GetAll");
            }
            return View();
        }
    }
}