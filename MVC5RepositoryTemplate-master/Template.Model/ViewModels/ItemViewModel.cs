using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Template.Data;

namespace Template.Model.ViewModels
{
    public class ItemViewModel
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ItemCode { get; set; }


        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
       // public SelectList CategoryName { get; set; }
        public virtual ICollection<Category> Category { get; set; }

        [Required]
        [Display(Name="Item Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Can be rented?")]
        public bool CanBeRented { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int QuantityInStock { get; set; }

        [Required]
        [Display(Name = "Item on rental")]
        public int OnRental { get; set; }

        [Required]
        [Display(Name = "Reserved for funeral")]
        public int ReservedForFuneral { get; set; }

        [Required]
        [Display(Name = "Re-order level")]
        public int ReorderLevel { get; set; }

        [Required]
        [Display(Name = "Picture")]
        public Byte[] Picture { get; set; }


        [Required]
        [Display(Name = "Item Price ")]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        //  public virtual ICollection<RentalItem> RentalItems { get; set; }
        public virtual ICollection<Category> Categorys { get; set; }
    }
}
