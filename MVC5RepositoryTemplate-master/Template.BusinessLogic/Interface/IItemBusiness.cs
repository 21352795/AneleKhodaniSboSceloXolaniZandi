using System.Collections.Generic;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
    public interface IItemBusiness
    {
        List<ItemViewModel> GetAllItems();
        void AddItem(ItemViewModel objItemView);
    }
}
