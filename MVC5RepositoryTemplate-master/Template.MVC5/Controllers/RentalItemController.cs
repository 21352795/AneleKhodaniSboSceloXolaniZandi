using System.Linq;
using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class RentalItemController : Controller
    {
        private readonly RentalItemBusiness _item = new RentalItemBusiness();

        //default
        public RentalItemController()
        {
        }

        //par
        public RentalItemController(RentalItemBusiness Item)
        {
            _item = Item;
        }


        #region add item
        public ActionResult Index()
        {
            return View(_item.GetAllRentalItems());
        }

        public ActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(RentalItemViewModel model)
        {
            {
                _item.AddRentalItem(model);


                return RedirectToAction("Index");
            }

        }

        #endregion


        #region update Item

        [HttpGet]
        public ActionResult EditItem(int id)
        {
            return View(_item.GetRentalItemsById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(RentalItemViewModel model)
        {

            _item.UpdateRentalItem(model);
            return RedirectToAction("Index");


        }
        #endregion


        #region delete item
        [HttpGet]
        public ActionResult DeleteItem(int id)
        {
            return View(_item.GetRentalItemsById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(RentalItemViewModel model, int id)
        {

            _item.DeleteRentalItem(id);

            RedirectToAction("Index");
            //Content("Sucessfull");
            return View();


        }
        #endregion

        #region  Search Item

        public ActionResult Search(string searchwith, string search)
        {
            ViewBag.s = search;
            var ai = from d in _item.GetAllRentalItems()
                     select d;

            if (searchwith == "ItemCode")
            {
                ai = ai.Where(d => d.ItemCode.ToString().Contains(search));
            }
            else if (searchwith == "rantalId")
            {
                ai = ai.Where(d => d.rentalId.ToString().Contains(search));
            }

            else if (ai.ToList().Count == 0)
            {
                ViewBag.E = "item not found";
            }



            return View(ai);
        }

        #endregion
    }
}