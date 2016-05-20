using System.Collections.Generic;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
    public interface ICategoryBusiness
    {
        List<CategoryViewModel> GetAllCategorys();
        void AddCategory(CategoryViewModel objCategoryView);
        void UpdateCategory(CategoryViewModel model);
        void DeleteCategory(int id);
    }
}
