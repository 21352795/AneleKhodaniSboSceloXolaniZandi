using System;
using System.ComponentModel.DataAnnotations;

namespace Template.Model.ViewModels
{
    public class RentalViewModel
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Hiring Id")]
        public string RentalId { get; set; }



        [Display(Name = "Date Hired")]
      // [DataType(DataType.Date)]
        public DateTime? DateRented { get; set; }

    
        [Display(Name = "Return date")]
       // [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Date require")]
        [DataType(DataType.Date)]
        public DateTime? DateRequire { get; set; }


        [Required]
        [Display(Name = "Fine")]
        [DataType(DataType.Currency)]
        public double Fine { get; set; }



        //[Required]
        //[Display(Name = "Category Name")]
        //public string  Category { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

       // [Required]
        [Display(Name = "Unit price ")]
        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }


        public string PolicyId { get; set; }
       // public virtual ICollection<Policy> Policy { get; set; }

        // public virtual ICollection<RentalPayment> RentalPayments { get; set; }
       // public virtual ICollection<RentalItem> RentalItems { get; set; }
    }
}