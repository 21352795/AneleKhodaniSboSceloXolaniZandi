using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class PolicyHolderController : Controller
    {
        // GET: PolicyHolder
        public ActionResult Index(string package,string gender, string province)
        {
            var phbusiness = new PolicyHolderBusiness();
            var phList = new List<PolicyHolderViewModel>();
            //if(!String.IsNullOrEmpty(package))
            //{
            //    phList = phbusiness.get_PH_ByPackage(package);
            //}
            if (!String.IsNullOrEmpty(gender))
            {
                phList = phbusiness.get_PH_ByGender(gender);
            }
            if (!String.IsNullOrEmpty(province))
            {
                phList = phbusiness.get_PH_ByProvince(province);
            }
            //phList = phbusiness.getAll();
            @ViewBag.Rec = phList.Count;
            return View(phList);
        }
        public ActionResult Applications()
        {
            var phbusiness = new PolicyHolderBusiness();
            return View(phbusiness.getApplications());
        }
    }
}