using System.Linq;
using System.Web.Mvc;
using Template.BusinessLogic.Implementation;

namespace Template.MVC5.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment

       
        public ActionResult Index()
        {

            return View();
        }
    
        readonly PaymentBusiness _pay = new PaymentBusiness();

        public ActionResult GetAllPayments()
        {
            var payment = _pay.GetAllPayments();
            if(payment==null)
            {
                return View("No Payment Found");
            }
            else
            {
                return View(payment);
            } 
        }
     
        public ActionResult SearchPayment(string searchid)
        {
            var search = from d in _pay.GetAllPayments() select d;
            search = search.Where(d => d.PolicyHolderIdNo.ToString().Contains(searchid));

            return View(search);
        }
    }
}