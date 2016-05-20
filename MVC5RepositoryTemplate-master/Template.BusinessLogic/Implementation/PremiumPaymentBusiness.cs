using System;
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
    public class PremiumPaymentBusiness : IPremiumPaymentBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }


        public DataContext con = new DataContext();

        public static string username { get; set; }
        public PremiumPaymentBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        public List<PremiumPaymentView> GetAllPremiumPayments()
        {
            using (var prerepo = new PremiumPaymentRepository())
            {
                return prerepo.GetAll().Select(x => new PremiumPaymentView()
                {
                    PolicyHolderIdNo = x.IDNumber,
                    Month = x.Month,
                    Date = x.Date,
                    AmountPaid = x.AmountPaid,
                    PaymentFor = x.PaymentFor,
                    PaymentMethod = x.PaymentMethod,
                    PaymentId = x.PaymentId,
                }).ToList();
            }
        }

        public void AddPremiumPayment(PremiumPaymentView objPremiumPaymentView)
        {
            using (var premiumrepo = new PremiumPaymentRepository())
            {
                var premium = new PremiumPayment
                {

                    IDNumber = objPremiumPaymentView.PolicyHolderIdNo,
                    AmountPaid = objPremiumPaymentView.AmountPaid,
                    Month = objPremiumPaymentView.Month,
                    Date = DateTime.Now,
                    PaymentFor = "Premium Payment",
                    PaymentMethod = "Cash",
                    PaymentId = objPremiumPaymentView.PaymentId
                };
                premiumrepo.Insert(premium);
            }
        }
    }
}
