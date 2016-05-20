using System.Linq;
using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Data;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class PremiumPaymentsController : Controller
    {

        // GET: PremiumPayment

        readonly PremiumPaymentBusiness _pre = new PremiumPaymentBusiness();

        public ActionResult GetAllPremiumPayments()
        {
            return View(_pre.GetAllPremiumPayments());

        }


        public ActionResult AddPremiumPayment()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPremiumPayment(PremiumPaymentView model)
        {
            var id = _pre.GetAllPremiumPayments().ToList().FindAll(x => x.PolicyHolderIdNo == model.PolicyHolderIdNo);
            DataContext con = new DataContext();
            var policyId = con.PolicyHolders.ToList().FindAll(x => x.IDNumber == model.PolicyHolderIdNo);
            if (policyId.Count ==0)
            {
                ViewBag.Error = "Policy Holder Does Not Exist - Invalid ID No.";
                return View(model);
            }

            if (!id.Equals(null))
            {
                foreach (var x in id)
                {
                    if (x.Month == model.Month)
                    {
                        ViewBag.Month = "You Already Made Payment For The Month of " + model.Month;
                        return View(model);
                    }
                 
                }
            }
          
            if (!policyId.Equals(null))
            {
                foreach (var x in policyId)
                {
                    if (x.packageID == 1)
                    {
                        if (model.AmountPaid <= 44)
                        {
                            ViewBag.Amount = "You can't pay monthly payment less than R45 for Standard Package.";
                            return View(model);
                        }
                    }
                    if (x.packageID == 2)
                    {
                        if (model.AmountPaid <= 54)
                        {
                            ViewBag.Amount = "You can't pay monthly payment less than R55 for Premium Package.";
                            return View(model);
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _pre.AddPremiumPayment(model);
                return RedirectToAction("GetAllPremiumPayments");
            }
            return View(model);
        }

        public ActionResult SearchPayment(string searchid)
        {
            var search = from d in _pre.GetAllPremiumPayments() select d;
            search = search.Where(d => d.PolicyHolderIdNo.ToString().Contains(searchid));

            return View(search);
        }
    }
}