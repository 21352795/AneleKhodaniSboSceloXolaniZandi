using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Data
{
   public class Payment
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }
        public string IDNumber { get; set; }
     

   
        public DateTime Date { get; set; }
        public Nullable<double> AmountPaid { get; set; }
        public string PaymentFor { get; set; }
        public string PaymentMethod { get; set; }
    }
}
