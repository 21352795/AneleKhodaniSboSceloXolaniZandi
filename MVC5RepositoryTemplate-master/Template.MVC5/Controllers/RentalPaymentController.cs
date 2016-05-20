using System.Linq;
using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Data;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class RentalPaymentController : Controller
    {
        readonly RentalPaymentBusiness _rent = new RentalPaymentBusiness();
        // GET: RentalPayment
        public ActionResult GetAllRentalPayments()
        {
            var rPay = _rent.GetAllRentalPayments();
            return View(rPay);

        }

        [HttpGet]
        public ActionResult AddRentalPayment(string id, string policyId , double totalprice)
        {
            RentalPaymentView rp = new RentalPaymentView();
         
            rp.RentalId = id;
            rp.PolicyHolderIdNo = policyId;
            rp.AmountPaid = totalprice;


            return View(rp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRentalPayment(RentalPaymentView model)
        {
            var r = new Rental();
            RentalBusiness rb = new RentalBusiness();
            var rv = rb.GetRentalById(model.RentalId);


            if (ModelState.IsValid)
            {
               // if (model.RentalId == rv.RentalId)
                {
                   // if (model.AmountPaid == rv.TotalPrice)
                    {
                        rb.Quantity(model.PolicyHolderIdNo);
                    }
                    
                }

                _rent.AddRentalPayment(model);
                return RedirectToAction("GetAllRentalPayments");

            }
            return View(model);
        }
  
        public ActionResult SearchRentalId()
        {
            var _r = new RentalBusiness();
            return View(_r.GetAllRentals());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchRentalId(string searchrentalid)
        {
            var _r = new RentalBusiness();
            var search = from c in _r.GetAllRentals() select c;
            search = search.Where(d => d.RentalId.Equals(searchrentalid));

            return View(search);
        }
        //Complete Payment

        //public ActionResult completePayment(int id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //}

    }
}