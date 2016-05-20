using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Model.ViewModels
{
    public class PersonalInfoViewModel
    {
        //personal information
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "Gender")]
        public string gender { get; set; }//
        [Display(Name = "Birthday")]
        public string dateOfBirth { get; set; }//
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Relationship")]
        public string spouse { get; set; }//married to?
        [Display(Name = "Lives in")]
        public string province { get; set; }
    }

    public class ContactDetailsViewModel
    {
        //contact information
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0]{1})\)?[-. ]?([1-9]{1})[-. ]?([0-9]{8})$", ErrorMessage = "Entered phone format is not valid.")]
        [StringLength(10, ErrorMessage = "SA Contact Number must be exactly 10 digits long", MinimumLength = 10)]
        [Display(Name = "Mobile")]
        public string contactNumber { get; set; }
        [Display(Name = "Email")]
        public string emailAdress { get; set; }
        [Required]
        [Display(Name = "Street Address")]
        [StringLength(50, ErrorMessage = "Street Address doesn't meet recommended length", MinimumLength = 3)]
        public string streetAddress { get; set; }
        [Required]
        [Display(Name = "Suburb")]
        [StringLength(50, ErrorMessage = "Suburb doesn't meet recommended length", MinimumLength = 3)]
        public string suburb { get; set; }
        [Required]
        [Display(Name = "City/Town")]
        [StringLength(50, ErrorMessage = "City/Town doesn't meet recommended length", MinimumLength = 3)]
        public string city { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"\d{4}", ErrorMessage = "SA Postal Code contains 4 digits only")]
        public int postalCode { get; set; }

        [Required]
        [Display(Name = "Postal Office")]
        [StringLength(50, ErrorMessage = "Postal Office doesn't meet recommended length", MinimumLength = 3)]
        public string postalOffice { get; set; }

        [Required]
        [Display(Name = "City/Town")]
        [StringLength(50, ErrorMessage = "City/Town doesn't meet recommended length", MinimumLength = 3)]
        public string town { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [StringLength(50, ErrorMessage = "City/Town doesn't meet recommended length", MinimumLength = 3)]
        [RegularExpression(@"\d{4}", ErrorMessage = "SA Postal Code contains 4 digits only")]
        public string boxpostalCode { get; set; }
    }

    public class PolicyDetailsViewModel
    {
        //Policy Details
        [Display(Name = "Policy No.")]
        public int policyNo { get; set; }
        //package info
        [Display(Name = "Package")]
        public string packageName { get; set; }
        //membership status
        [Display(Name = "Member since")]
        public DateTime dateStarted { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }

        //policy documents
    }

    public class BeneficiariesViewModel
    {
        [Display(Name = "ID Number")]
        [RegularExpression(@"^([0-9]){2}([0-1][0-9])([0-3][0-9])([0-9]){4}([0-1])([0-9]){2}?$", ErrorMessage = "Not yet valid.")]
        [StringLength(13, ErrorMessage = "SA ID Number must be exactly 13 digits long", MinimumLength = 13)]
        public string benIDNumber { get; set; }//beneficiary IDNumber
        // public string IDNumber { get; set; }//applicant IDNumber - foreign Key
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Relationship")]
        public string relationship { get; set; }

        [Display(Name = "Age")]
        public int age { get; set; }
    }

    public class PersonalPaymentHistory
    {
    
        [DisplayName("Payment ID")]
        public int PaymentId { get; set; }

        [DisplayName("Date and Time")]
        public DateTime Date { get; set; }

        [DisplayName("Month")]
        public string Month { get; set; }

        [DisplayName("Amount Paid")]
        [DataType(DataType.Currency)]
        public Nullable<double> AmountPaid { get; set; }    
            
        [DisplayName("Payment For")]
        public string PaymentFor { get; set; }

        [DisplayName("Payment Method Used")]
        public string PaymentMethod { get; set; }
    }
    public class EventLogViewModel
    {
        [Display(Name ="Event ID")]
        public int eventID { get; set; }
        [Display(Name = "Activity")]
        public string Activity { get; set; }
        [Display(Name = "Date")]
        public string EventDate { get; set; }
        public string Time { get; set; }
    }
}
