using System.Collections.Generic;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
   public interface IPremiumPaymentBusiness
    {
        List<PremiumPaymentView> GetAllPremiumPayments();
        void AddPremiumPayment(PremiumPaymentView objRentalPaymentView);
    }
}
