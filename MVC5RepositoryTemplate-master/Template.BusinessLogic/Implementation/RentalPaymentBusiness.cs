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
    public class RentalPaymentBusiness : IRentalPaymentBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }


        public DataContext con = new DataContext();

        public static string username { get; set; }
        public RentalPaymentBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        public List<RentalPaymentView> GetAllRentalPayments()
        {
            using (var rentalrepo = new RentalPaymentRepository())
            {
                return rentalrepo.GetAll().Select(x => new RentalPaymentView()
                {
                    PolicyHolderIdNo=x.IDNumber,
                    RentalId = x.RentalId,                 
                    Date=x.Date,
                    AmountPaid=x.AmountPaid,
                    PaymentFor=x.PaymentFor,
                    PaymentMethod=x.PaymentMethod,
                    PaymentId=x.PaymentId,
                    


                }).ToList();
            }
        }

        public void AddRentalPayment(RentalPaymentView objRentalPaymentView)
        {
           
            

            using (var rentalrepo = new RentalPaymentRepository())
            {
               


                var rent = new RentalPayment {
                    IDNumber=objRentalPaymentView.PolicyHolderIdNo,
                   RentalId=objRentalPaymentView.RentalId,                  
                   AmountPaid=objRentalPaymentView.AmountPaid,
                   Date=DateTime.Now,                  
                   PaymentFor="Rental",
                   PaymentMethod="Cash",
                  
                   PaymentId=objRentalPaymentView.PaymentId,
            };


             

                rentalrepo.Insert(rent);
             
            }


        }
        //public double fine(RentalPaymentView pv)
        //{

        //}
      

    }
}
