using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Template.Data
{
    public class Rental
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RentalId { get; set; }
      
        public DateTime? DateRented { get; set; }
      
        public DateTime? DateRequire { get; set; }

        public DateTime? ReturnDate { get; set; }
        public double Fine { get; set; }

        public string PolicyId { get; set; }

       // public string Category { get; set; }

        public double Totalprice { get; set; }

        public int Quantity { get; set; }
        public string  ItemCode { get; set; }


       // public virtual ICollection<RentalPayment> RentalPayments { get; set; }
        public virtual ICollection<RentalItem> RentalItems { get; set; }
    }
}