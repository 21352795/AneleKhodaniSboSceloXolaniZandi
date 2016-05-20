using System.Collections.Generic;
using Template.Model.ViewModels;

namespace Template.BusinessLogic.Interface
{
    public interface IPaymentBusiness
    {
        List<PaymentView> GetAllPayments();
      
    }
}
