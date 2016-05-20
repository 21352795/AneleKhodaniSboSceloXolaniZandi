using System.Collections.Generic;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
    public interface IRentalBusiness
    {
        List<RentalViewModel> GetAllRentals();
        void AddRental(RentalViewModel objRentalView);
    }
}
