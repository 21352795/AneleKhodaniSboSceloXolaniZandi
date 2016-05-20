using System.Web.Mvc;
using Template.BusinessLogic.Implementation;
using Template.Model.ViewModels;

namespace Template.MVC5.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private readonly CategoryBusiness _Category = new CategoryBusiness();

        //default
        public CategoryController()
        {
        }

        //par
        public CategoryController(CategoryBusiness Category)
        {
            _Category = Category;
        }

      

        public ActionResult Index()
        {
            return View(_Category.GetAllCategorys());
        }



        #region add category
       
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(CategoryViewModel model)
        {
           // if (ModelState.IsValid)
            {
                
            _Category.AddCategory(model);
            return RedirectToAction("Index");
            }

          //  return View();
        }
         #endregion


        #region update category

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            return View(_Category.GetCategoryById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(CategoryViewModel model)
        {
          //  if (ModelState.IsValid)
            {
                _Category.UpdateCategory(model);
                return RedirectToAction("Index");
            }
          //  return View(model);
        }
        #endregion

        #region delete category
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            return View(_Category.GetCategoryById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(CategoryViewModel model, int id)
        {
           // _Category.GetCategoryById(id);
            _Category.DeleteCategory(id);

            RedirectToAction("Index");
            //Content("Sucessfull");
            return View();


        }
        #endregion





    }
}