using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class RentalController : Controller
    {

        private readonly RentalBusiness _Rental = new RentalBusiness();
        private readonly ItemBusiness _item = new ItemBusiness();


        //  RentalViewModel RentalL = new RentalViewModel();
        //default
        public RentalController()
        {

        }

        //par
        public RentalController(RentalBusiness Rental)
        {
            _Rental = Rental;
        }


        #region add Rental

        public ActionResult Index()
        {
            return View(_Rental.GetAllRentals());
        }
      
            
        public ActionResult AddRental(string id,double price, string name)
        {
            RentalViewModel r = new RentalViewModel();
            r.PolicyId = HttpContext.User.Identity.Name;
            r.ItemCode = id;
            r.TotalPrice = price;
            r.ItemName = name;


            return View(r);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRental(RentalViewModel model)
        {
            RentalViewModel r = new RentalViewModel();
            r.PolicyId = HttpContext.User.Identity.Name;
            ViewBag.Itemcode = _item.CanBeRentedItems();


            if (!_Rental.ItemAvailability(model).Equals("no error"))
            {
                ViewBag.ItemAvailability = _Rental.ItemAvailability(model);
                return View();

            }
            if (model.Quantity <= 0)
            {
                ViewBag.ItemAvailability = "Quantity must be greater than 0";
                return View();
            }

            if (model.DateRequire <= System.DateTime.Now.Date)
            {
                ViewBag.Calender = "Date required cannot be today's or previous date";
                return View();
            }

            if (model.Quantity > 0)
            {
                _Rental.AddRental(model);
            }

            return RedirectToAction("Details", "Rental", new {id = _Rental.getRentalID(HttpContext.User.Identity.Name)});

        }



        #endregion


        #region update Rental

        [HttpGet]
        public ActionResult EditRental(string id)
        {
            return View(_Rental.GetRentalById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRental(RentalViewModel model)
        {

            _Rental.UpdateRental(model);
            return RedirectToAction("Index");


        }

        #endregion


        #region delete Rental

        [HttpGet]
        public ActionResult DeleteRental(string id)
        {
            return View(_Rental.GetRentalById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRental(RentalViewModel model, string id)
        {

            _Rental.DeleteRental(id);

            RedirectToAction("Index");
            //Content("Sucessfull");
            return View();


        }

        #endregion

        public ActionResult ShowItems()
        {
            var _item = new ItemBusiness();
            return View(_item.ShowItems());
        }

        public ActionResult Details(string id)
        {   

            return View(_Rental.Details(id));
        }


        //#region  Search Rental
        //public ActionResult Search(string searchwith, string search)
        //{
        //    var allRentals = from d in _Rental.GetAllRentals()
        //                   select d;

        //    if (searchwith == "RentalName")
        //    {
        //        allRentals = allRentals.Where(d => d.Name.ToString().Contains(search));
        //    }
        //    else
        //        if (searchwith == "RentalCode")
        //    {
        //        allRentals = allRentals.Where(d => d.RentalCode.ToString().Contains(search));
        //    }


        //    return View("Index");
        //}
        //#endregion

    }
}
