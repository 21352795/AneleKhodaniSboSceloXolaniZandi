using System.Collections.Generic;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
    public interface IRentalPaymentBusiness
    {
        List<RentalPaymentView> GetAllRentalPayments();
        void AddRentalPayment(RentalPaymentView objRentalPaymentView);
      
    }
}
