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
    public class PaymentBusiness : IPaymentBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }


      

        
        public PaymentBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        public List<PaymentView> GetAllPayments()
        {
            using (var paymentrepo = new PaymentRepository())
            {
                return paymentrepo.GetAll().Select(x => new PaymentView() { PaymentId = x.PaymentId, PolicyHolderIdNo = x.IDNumber, PaymentMethod = x.PaymentMethod, AmountPaid = x.AmountPaid, PaymentFor = x.PaymentFor, Date=x.Date }).ToList();
            }
        }

      


        }
      
     
    }

