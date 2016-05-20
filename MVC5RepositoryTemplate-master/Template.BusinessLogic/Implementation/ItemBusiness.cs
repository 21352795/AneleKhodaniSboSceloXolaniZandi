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
    public class ItemBusiness : IItemBusiness
    {


        public UserManager<ApplicationUser> UserManager { get; set; }

        public ItemBusiness()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DataContext()));
        }

        #region get all items

        public List<ItemViewModel> GetAllItems()
        {
            using (var itemrepo = new ItemRepository())
            {
                return itemrepo.GetAll().Select(x => new ItemViewModel()
                {
                    ItemCode = x.ItemCode,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryName = x.CategoryName,
                    QuantityInStock = x.QuantityInStock,
                    CanBeRented = x.CanBeRented,
                    OnRental = x.OnRental,
                    ReservedForFuneral = x.ReservedForFuneral,
                    ReorderLevel = x.ReorderLevel,
                    Picture = x.Picture,
                    Price = x.Price,

                }).ToList();
            }
        }

        #endregion

        public List<ItemViewModel> ShowItems()
        {
            using (var itemrepo = new ItemRepository())
            {
                return itemrepo.GetAll().Select(x => new ItemViewModel()
                {
                    ItemCode = x.ItemCode,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryName = x.CategoryName,
                    // QuantityInStock = x.QuantityInStock,
                    //  CanBeRented = x.CanBeRented,
                    //  OnRental = x.OnRental,
                    //  ReservedForFuneral = x.ReservedForFuneral,
                    //   ReorderLevel = x.ReorderLevel,
                    Picture = x.Picture,
                    Price = x.Price,

                }).ToList();
            }
        }

        #region get item by id

        public ItemViewModel GetItemById(string id)
        {
            using (var Item = new ItemRepository())
            {
                Item itm = Item.GetById(id);
                var itmViewModel = new ItemViewModel();
                if (itm != null)
                {

                    itmViewModel.ItemCode = itm.ItemCode;
                    itmViewModel.CategoryName = itm.CategoryName;
                    itmViewModel.Description = itm.Description;
                    itmViewModel.Name = itm.Name;
                    itmViewModel.CanBeRented = itm.CanBeRented;
                    itmViewModel.OnRental = itm.OnRental;
                    itmViewModel.QuantityInStock = itm.QuantityInStock;
                    itmViewModel.ReorderLevel = itm.ReorderLevel;
                    itmViewModel.ReservedForFuneral = itm.ReservedForFuneral;
                    itmViewModel.Picture = itm.Picture;
                    itmViewModel.Price = itm.Price;

                }
                return itmViewModel;
            }
        }

        #endregion

        #region generate itemcode


        public string itemCode(int CodeLength)
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

        public static string feedback;

        #region upload picture

        public string UploadPicture(ItemViewModel model)
        {
            try
            {

                //foreach (DocumentViewModel doc in model)
                //{
                Item objP = new Item()
                {
                    Picture = model.Picture,
                };
                using (var itemRepo = new ItemRepository())
                {

                    itemRepo.Insert(objP);
                }
                //}
            }
            catch (Exception ex)
            {
                feedback = "Unable to upload a document, our servers are currently down, please try again.";
            }
            return feedback;
        }

        #endregion

        #region add item

        public void AddItem(ItemViewModel objItemView)
        {
            using (var itemrepo = new ItemRepository())
            {


                var Item = new Item
                {
                    ItemCode = itemCode(4),
                    Name = objItemView.Name,
                    Description = objItemView.Description,
                    CategoryName = objItemView.CategoryName,
                    QuantityInStock = objItemView.QuantityInStock,
                    CanBeRented = objItemView.CanBeRented,
                    OnRental = objItemView.OnRental,
                    ReservedForFuneral = objItemView.ReservedForFuneral,
                    ReorderLevel = objItemView.ReorderLevel,
                    Picture = objItemView.Picture,
                    Price = objItemView.Price,
                };

                itemrepo.Insert(Item);
            }
        }

        #endregion

        #region update item

        public void UpdateItem(ItemViewModel model)
        {
            using (var Item = new ItemRepository())
            {
                Item itm = Item.GetById(model.ItemCode);
                if (itm != null)
                {


                    itm.ItemCode = model.ItemCode;
                    itm.CategoryName = model.CategoryName;
                    itm.Description = model.Description;
                    itm.Name = model.Name;
                    itm.CanBeRented = model.CanBeRented;
                    itm.OnRental = model.OnRental;
                    itm.QuantityInStock = model.QuantityInStock;
                    itm.ReorderLevel = model.ReorderLevel;
                    itm.ReservedForFuneral = model.ReservedForFuneral;
                    itm.Picture = model.Picture;
                    itm.Price = model.Price;

                    Item.Update(itm);
                }
            }
        }

        #endregion

        #region search item

        public ItemViewModel Search(string id)
        {
            using (var i = new ItemRepository())
            {
                Item obj = i.GetById(id);
                var itm = new ItemViewModel();

                itm.ItemCode = obj.ItemCode;
                itm.CategoryName = obj.CategoryName;
                itm.Description = obj.Description;
                itm.Name = obj.Name;
                itm.Picture = obj.Picture;
                itm.QuantityInStock = obj.QuantityInStock;
                itm.OnRental = obj.OnRental;
                itm.ReservedForFuneral = obj.ReservedForFuneral;
                itm.CanBeRented = obj.CanBeRented;
                itm.Price = obj.Price;

                return itm;
            }
        }

        #endregion

        #region delete Item

        public void DeleteItem(string id)
        {
            using (var Item = new ItemRepository())
            {
                Item ctg = Item.GetById(id);
                if (ctg != null)
                {
                    Item.Delete(ctg);
                }
            }
        }

        #endregion

       

        public List<ItemViewModel> CanBeRentedItems()
        {
            var itemslist = new List<ItemViewModel>();
            foreach(ItemViewModel item in GetAllItems())
            {
                if (item.CanBeRented == true)
                {
                    itemslist.Add(item);
                } 
            }

            return itemslist;
        }
    }
}
