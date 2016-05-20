using System.Linq;
using System.Web;
using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemBusiness _item = new ItemBusiness();
        private readonly CategoryBusiness _category = new CategoryBusiness();
        //  ItemViewModel itemL = new ItemViewModel();
        //default
        public ItemController()
        {
        }

        //par
        public ItemController(ItemBusiness Item)
        {
            _item = Item;
        }


        #region add item
        public ActionResult Index()
        {
            return View(_item.GetAllItems());
        }

        public ActionResult AddItem()
        {
            ViewBag.Category = _category.GetAllCategorys();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(ItemViewModel model, HttpPostedFileBase upload)
        {
            {
                ViewBag.Category = _category.GetAllCategorys();
                if (model.QuantityInStock <= 0 )
                {
                    ViewBag.Q = "Quantity must be greater than 0";
                    return View();
                }
                if (model.Price <= 0)
                {
                    ViewBag.P = "Item price must be greater than 0";
                    return View();
                }
                int filelength = upload.ContentLength;
                byte[] array = new byte[filelength];
                upload.InputStream.Read(array, 0, filelength);
                model.Picture = array;

                _item.AddItem(model);
                return RedirectToAction("Index");
            }

        }

        #endregion


        #region update Item

        [HttpGet]
        public ActionResult EditItem(string id)
        {
            return View(_item.GetItemById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(ItemViewModel model)
        {
            
                _item.UpdateItem(model);
                return RedirectToAction("Index");
          
           
        }
        #endregion


        #region delete item
        [HttpGet]
        public ActionResult DeleteItem(string id)
        {
            return View(_item.GetItemById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(ItemViewModel model, string id)
        {
        
            _item.DeleteItem(id);

            RedirectToAction("Index");
            //Content("Sucessfull");
            return View();


        }
        #endregion

        #region  Search Item

        public ActionResult SearchItem()
        {
            var _i = new ItemBusiness();
            return View(_i.GetAllItems());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchItem(string ItemName)
        {
            var _i = new ItemBusiness();
            var search = from c in _i.GetAllItems() select c;

            if (!search.Equals(null))
            {
                search = search.Where(d => d.Name.Equals(ItemName));
            }
            else
            {
                ViewBag.Error = "Item not found";
                return View();
            }

            return View(search);
        }
        #endregion

        public ActionResult AddPicture(ItemViewModel model, HttpPostedFileBase upload)
        {
           // if (ModelState.IsValid)
            {
                int filelength = upload.ContentLength;
                byte[] array = new byte[filelength];
                upload.InputStream.Read(array, 0, filelength);
                model.Picture = array;
                var obji = new ItemBusiness();
               obji.UploadPicture(model);
            }
            return View("AddItem");
        }


    }
}