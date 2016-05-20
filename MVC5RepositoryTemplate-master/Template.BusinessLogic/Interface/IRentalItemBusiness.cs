using System.Collections.Generic;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
    public interface IRentalItemBusiness
    {
        List<RentalItemViewModel> GetAllRentalItems();
        void AddRentalItem(RentalItemViewModel objRentalItemView);
        //  void UpdateRental(RentalViewModel model);
        // void DeleteRental(int id);
    }
}
