using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Template.BusinessLogic.Interface;
using Template.Data;
using Template.Model.ViewModels;
using Template.Service.Implementation;

namespace Template.BusinessLogic.Implementation
{
    public class CategoryBusiness : ICategoryBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }

        public CategoryBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        public List<CategoryViewModel> GetAllCategorys()
        {
            using (var categoryrepo = new CategoryRepository())
            {
                return categoryrepo.GetAll().Select(x => new CategoryViewModel() { CategoryId = x.CategoryId, CategoryName = x.CategoryName, Description = x.Description}).ToList();
            }
        }
        #region CRUD

        #region add category
        public void AddCategory(CategoryViewModel objCategoryView)
        {
            using (var categoryrepo = new CategoryRepository())
            {
               

                var category = new Category
                {   CategoryId = objCategoryView.CategoryId,
                    CategoryName = objCategoryView.CategoryName,
                    Description = objCategoryView.Description  };
              
                categoryrepo.Insert(category);
            }
        }

       

        #endregion


        #region get category by id

        public CategoryViewModel GetCategoryById(int id)
        {
            using (var Category = new CategoryRepository())
            {
                Category ctg = Category.GetById(id);
                var ctgViewModel = new CategoryViewModel();
                if (ctg != null)
                {
                    ctgViewModel.CategoryId = ctg.CategoryId;
                    ctgViewModel.CategoryName = ctg.CategoryName;
                    ctgViewModel.Description = ctg.Description;
                }
                return ctgViewModel;
            }
        }
        #endregion

        #region update category
        public void UpdateCategory(CategoryViewModel model)
        {
            using (var Category = new CategoryRepository())
            {
                Category ctg = Category.GetById(model.CategoryId);
                if (ctg != null)
                {
                    ctg.CategoryId = model.CategoryId;
                    ctg.CategoryName = model.CategoryName;
                    ctg.Description = model.Description;

                    Category.Update(ctg);
                }
            }
        }
#endregion

        #region delete category
        public void DeleteCategory(int id)
        {
            using (var Category = new CategoryRepository())
            {
                Category ctg = Category.GetById(id);
                if (ctg != null)
                {
                    Category.Delete(ctg);
                }
            }
        }

        #endregion



        #endregion


    }
}
