using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Data;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class PackageController : Controller
    {
        // GET: Package

        readonly PackageBusiness prepo = new PackageBusiness();
        
        public ActionResult GetAll()
        {
            return View(prepo.GetAll());
        }

        [HttpGet]
        public ActionResult AddPackage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPackage(PackageView model)
        {
            if (ModelState.IsValid)
            {
                prepo.AddPackage(model);
                var pb = new PackageBusiness();
                return RedirectToAction("AddPackageBenefits", new { packID = pb.GetByName(model.Name).PackageId });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditPackage(int id)
        {
            return View(prepo.GetById(id));
        }

        [HttpPost, ActionName("EditPackage")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PackageView model)
        {
            prepo.UpdatePackage(model);
            return RedirectToAction("GetAll");
        }

        public ActionResult Search(string name)
        {
            return View(prepo.GetByName(name));
        }

        public ActionResult Details(int id)
        {
            return View(prepo.Details(id));
        }
        //Get : Benefits
        public ActionResult OfferedBenefits()
        {
            return View(prepo.GetAllBenefits());
        }
        public ActionResult AddBenefit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBenefit(BenefitView model)
        {
            if (ModelState.IsValid)
            {
                prepo.AddBenefit(model);
                return RedirectToAction("OfferedBenefits");
            }
            return View(model);
        }
        //Get : AddPackageBenefit
        public ActionResult AddPackageBenefits(int? packID)
        {
            if(packID ==null)
            {
                return HttpNotFound();
            }
            var pb = new PackageBusiness();
            var pbv = new Template.Model.ViewModels.PackageBenefitView();
            pbv.benefit = new List<Template.Model.ViewModels.BenefitView>();
            pbv.benefit = pb.GetAllBenefits();
            Session["packID"] = packID;
            Session["Name"] = pb.GetById((int)packID).Name;
            Session["PremiumAmount"] = pb.GetById((int)packID).PremiumAmount;
            Session["maxBeneficiary"] = pb.GetById((int)packID).maxBeneficiary;

            List<BenefitView> benefit = new List<BenefitView>();
            benefit = pb.GetAllBenefits();
            return View(benefit);
        }
        [HttpPost]
        public ActionResult AddPackageBenefits(List<BenefitView> model)
        {
            var pb = new PackageBusiness();            
            pb.AddPackageBenefit(model, Convert.ToInt16(Session["packID"]));           
            return RedirectToAction("GetAll");
        }

        public ActionResult OfferedPackages()
        {
            var pb = new PackageBusiness();
            return View(pb.GetPackagesWithBenefits());
        }
    }
}