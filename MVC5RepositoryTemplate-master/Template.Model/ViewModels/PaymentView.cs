using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Model.ViewModels
{
   public class PaymentView
    {

        [Key]
        [DisplayName("Payment ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [Required]
        [Display(Name = "ID Number")]
        [RegularExpression(@"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))", ErrorMessage = "Invalid.")]
        [StringLength(13, ErrorMessage = "SA ID Number must be exactly 13 digits long", MinimumLength = 13)]
        public string PolicyHolderIdNo { get; set; }


        [DisplayName("Date and Time")]
        public DateTime Date { get; set; }


        [DisplayName("Amount Paid")]

        [DataType(DataType.Currency)]
        public Nullable<double> AmountPaid { get; set; }

        [DisplayName("Payment For")]

        public string PaymentFor { get; set; }
        [DisplayName("Payment Method Used")]
        public string PaymentMethod { get; set; }
    }
}
