using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Template.Data
{
    public class Item
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ItemCode { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanBeRented { get; set; }
        public int QuantityInStock { get; set; }
        public int OnRental { get; set; }
        public int ReservedForFuneral { get; set; }
        //public int ReorderLevel { get; set; }
        public Byte []Picture { get; set; }
        public double Price { get; set; }

        public virtual ICollection<RentalItem> RentalItems { get; set; }
        public virtual ICollection<Category> Categorys { get; set; }
    }

}