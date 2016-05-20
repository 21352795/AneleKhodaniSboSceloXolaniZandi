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
    public class RentalItemBusiness : IRentalItemBusiness
    {
        public UserManager<ApplicationUser> UserManager { get; set; }

        public RentalItemBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }


        public List<RentalItemViewModel> GetAllRentalItems()
        {
            using (var Rentalrepo = new RentalItemRepository())
            {
                return Rentalrepo.GetAll().Select(x => new RentalItemViewModel()
                {
                    RentaItemId = x.RentaItemId,
                    rentalId=x.rentalId,
                    ItemCode=x.ItemCode,
                    Quantity=x.Quantity
                

                }).ToList();
            }
        }

        //  #region CRUD

        public static int code = 100;
        public string itemCode()
        {
            code = code++;

            return "r" + code++;
        }

        #region add Rental
        public void AddRentalItem(RentalItemViewModel objRentalView)
        {
            using (var Rentalrepo = new RentalItemRepository())
            {


                var RentalItem = new RentalItem
                {   //RentalId = itemCode(),
                    rentalId = objRentalView.rentalId,
                    ItemCode = objRentalView.ItemCode,
                    Quantity = objRentalView.Quantity 
                };

                Rentalrepo.Insert(RentalItem);
            }
        }



        #endregion


        #region get Rental by id

        public RentalItemViewModel GetRentalItemsById(int id)
        {
            using (var Rental = new RentalItemRepository())
            {
                RentalItem rtl = Rental.GetById(id);
                var rtlViewModel = new RentalItemViewModel();
                if (rtl != null)
                {
                    rtlViewModel.RentaItemId = rtl.RentaItemId;
                    rtlViewModel.rentalId = rtl.rentalId;
                    rtlViewModel.ItemCode = rtl.ItemCode;

                    rtlViewModel.Quantity = rtl.Quantity;
             

                }
                return rtlViewModel;
            }
        }
        #endregion

        #region update Rental
        public void UpdateRentalItem(RentalItemViewModel model)
        {
            using (var Rental = new RentalItemRepository())
            {
                RentalItem rtl = Rental.GetById(model.RentaItemId);
                if (rtl != null)
                {
                    rtl.RentaItemId = model.RentaItemId;
                    rtl.rentalId = model.rentalId;
                    rtl.ItemCode = model.ItemCode;

                    rtl.Quantity = model.Quantity;

                    Rental.Update(rtl);
                }
            }
        }
        #endregion

        #region delete Rental
        public void DeleteRentalItem(int id)
        {
            using (var Rental = new RentalItemRepository())
            {
                RentalItem rtl = Rental.GetById(id);
                if (rtl != null)
                {
                    Rental.Delete(rtl);
                }
            }
        }

        #endregion




    }
}

