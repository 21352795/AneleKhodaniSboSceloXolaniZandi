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
    public class RentalBusiness : IRentalBusiness
    {


        public UserManager<ApplicationUser> UserManager { get; set; }

        public RentalBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        #region get all Rentals

        public List<RentalViewModel> GetAllRentals()
        {
            using (var Rentalrepo = new RentalRepository())
            {
                return Rentalrepo.GetAll().Select(x => new RentalViewModel()
                {
                    RentalId = x.RentalId,
                    DateRented = x.DateRented,
                    DateRequire = x.DateRequire.GetValueOrDefault(),
                    ReturnDate = x.ReturnDate,
                    Fine = x.Fine,
                    PolicyId = x.PolicyId,
                    TotalPrice = x.Totalprice,
                    ItemCode = x.ItemCode,
                    Quantity = x.Quantity,

                }).ToList();
            }
        }

        #endregion

        #region get Rental by id

        public string getRentalID(string id)
        {
            using (var rental = new RentalRepository())
            {
                return rental.Find(x => x.PolicyId == id).Last().RentalId;
            }
        }

        public RentalViewModel GetRentalById(string id)
        {
            using (var Rental = new RentalRepository())
            {
                Rental itm = Rental.GetById(id);
                var itmViewModel = new RentalViewModel();
                if (itm != null)
                {


                    itmViewModel.RentalId = itm.RentalId;
                    itmViewModel.DateRented = itm.DateRented.GetValueOrDefault();
                    itmViewModel.DateRequire = itm.DateRequire.GetValueOrDefault();
                    itmViewModel.ReturnDate = itm.ReturnDate;
                    itmViewModel.Fine = itm.Fine;
                    itmViewModel.PolicyId = itm.PolicyId;
                    itmViewModel.TotalPrice = itm.Totalprice;
                    itmViewModel.ItemCode = itm.ItemCode;
                    itmViewModel.Quantity = itm.Quantity;

                }
                return itmViewModel;
            }
        }

        #endregion

        #region generate Rentalcode


        public string RentalCode(int CodeLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[CodeLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < CodeLength; i++)
            {
                chars[i] = _allowedChars[(int) ((_allowedChars.Length)*randNum.NextDouble())];
            }
            return new string(chars);
        }

        #endregion



        public double calcTotPrice(RentalViewModel q)
        {
            using (var itemRepo = new ItemRepository())
            {
                Item item = itemRepo.GetById(q.ItemCode);
                double tot = item.Price*q.Quantity;
                return tot;
            }

        }

        #region add Rental

        public void AddRental(RentalViewModel objRentalView)
        {
            var RenatlItemrepo = new RentalItemRepository();
            using (var Rentalrepo = new RentalRepository())
            {
                Item p = new Item();

                var Rental = new Rental
                {
                    RentalId = RentalCode(4),
                    DateRented = DateTime.Now,
                    DateRequire = objRentalView.DateRequire,
                    ReturnDate = objRentalView.ReturnDate,
                    Fine = objRentalView.Fine,
                    PolicyId = objRentalView.PolicyId,
                    Totalprice = calcTotPrice(objRentalView),
                    ItemCode = objRentalView.ItemCode,
                    Quantity = objRentalView.Quantity,

                };

                var riv = new RentalItem()
                {
                    rentalId = Rental.RentalId,
                    ItemCode = objRentalView.ItemCode,
                    Quantity = objRentalView.Quantity,
                };
                Rentalrepo.Insert(Rental);
                RenatlItemrepo.Insert(riv);
            }
        }

        #endregion

        #region update Rental

        public void UpdateRental(RentalViewModel model)
        {
            using (var Rental = new RentalRepository())
            {
                Rental itm = Rental.GetById(model.RentalId);
                if (itm != null)
                {



                    itm.RentalId = model.RentalId;
                    itm.DateRented = model.DateRented;
                    itm.DateRequire = model.DateRequire;
                    itm.ReturnDate = DateTime.Now;
                    itm.Fine = model.Fine;
                    itm.PolicyId = model.PolicyId;
                    itm.Totalprice = model.TotalPrice;
                    itm.ItemCode = model.ItemCode;
                    itm.Quantity = model.Quantity;



                    Rental.Update(itm);
                }
            }
        }

        #endregion

        #region search Rental

        public RentalViewModel Search(string id)
        {
            using (var i = new RentalRepository())
            {
                Rental obj = i.GetById(id);
                var itm = new RentalViewModel();


                itm.RentalId = obj.RentalId;
                itm.DateRented = obj.DateRented.GetValueOrDefault();
                itm.DateRequire = obj.DateRequire.GetValueOrDefault();
                itm.ReturnDate = obj.ReturnDate;
                itm.Fine = obj.Fine;
                itm.PolicyId = obj.PolicyId;
                itm.TotalPrice = obj.Totalprice;
                itm.ItemCode = obj.ItemCode;
                itm.Quantity = obj.Quantity;

                return itm;
            }
        }

        #endregion

        #region delete Rental

        public void DeleteRental(string id)
        {
            using (var Rental = new RentalRepository())
            {
                Rental ctg = Rental.GetById(id);
                if (ctg != null)
                {
                    Rental.Delete(ctg);
                }
            }
        }

        #endregion


        public RentalViewModel Details(string id)
        {
            RentalViewModel pv = GetRentalById(id);
            return pv;
        }

        public string ItemAvailability(RentalViewModel model)
        {
            string feedback = "no error";
            var itemRepo = new ItemRepository();
            //   ItemViewModel itm = new ItemViewModel();

            Item item = itemRepo.GetById(model.ItemCode);
            if (item.ItemCode == model.ItemCode)
            {
                if (model.Quantity > item.QuantityInStock)
                {
                    feedback = "sorry , currently there are only " + item.QuantityInStock + " " + item.Name +
                               " available";
                }
            }

            else if (item.QuantityInStock <= 0)
            {
                feedback = "Item not available for rental";
            }

            //else
            //{
            //    var itemO = new Item()
            //    {
            //        QuantityInStock = item.QuantityInStock - model.Quantity
            //    };

            //    itemRepo.Insert(model);
            //}
            return feedback;
        }

        public void Quantity(string id)
        {
             int qOnstock = 0;
            using (var itemRepo = new ItemRepository())
            {
              
                RentalBusiness _rental = new RentalBusiness();
                ItemBusiness _Item = new ItemBusiness();

                foreach (RentalViewModel rentals in GetAllRentals())
                {
                    if (rentals.PolicyId == id)
                    {
                        var qr = _rental.GetRentalById(rentals.RentalId);
                        var item = _Item.GetItemById(rentals.ItemCode);
                        qOnstock = item.QuantityInStock - qr.Quantity;

                        var itemI = new Item()
                        {
                            QuantityInStock = qOnstock,

                        };
                        itemRepo.Update(itemI);
                        //  item.QuantityInStock = qOnstock;
                    };

                     

                      
                    }

                }
            }
            

        }

    }

