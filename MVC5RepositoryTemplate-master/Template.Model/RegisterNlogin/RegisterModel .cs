using System.ComponentModel.DataAnnotations;

namespace Template.Model.RegisterNlogin
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "ID Number")]
        [RegularExpression(@"^([0-9]){2}([0-1][0-9])([0-3][0-9])([0-9]){4}([0-1])([0-9]){2}?$", ErrorMessage = "Not yet valid.")]
        [StringLength(13, ErrorMessage = "SA ID Number must be exactly 13 digits long", MinimumLength = 13)]
        public string identityNumber { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(35, ErrorMessage = "Fist Name must be atleast 3 characters long", MinimumLength = 3)]
        public string firstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Numbers and special characters are not allowed.")]
        [StringLength(35, ErrorMessage = "Last Name must be atleast 3 characters long", MinimumLength = 3)]
        public string lastName { get; set; }

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
        [RegularExpression(@"\d{4}",ErrorMessage ="SA Postal Code contains 4 digits only")]
        public int postalCode { get; set; }

        //[Required]
        [Display(Name = "Postal Office")]
        [StringLength(50, ErrorMessage = "Postal Office doesn't meet recommended length", MinimumLength = 3)]
        public string postalOffice { get; set; }

        //[Required]
        [Display(Name = "City/Town")]
        [StringLength(50, ErrorMessage = "City/Town doesn't meet recommended length", MinimumLength = 3)]
        public string town { get; set; }

        //[Required]
        [Display(Name = "Postal Code")]
        [StringLength(50, ErrorMessage = "City/Town doesn't meet recommended length", MinimumLength = 3)]
        [RegularExpression(@"\d{4}", ErrorMessage = "SA Postal Code contains 4 digits only")]
        public string boxpostalCode { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$",ErrorMessage ="Email not valid")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Province")]
        public string province { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0]{1})\)?[-. ]?([1-9]{1})[-. ]?([0-9]{8})$", ErrorMessage = "Entered phone format is not valid.")]
        [StringLength(10, ErrorMessage = "SA Contact Number must be exactly 10 digits long", MinimumLength = 10)]
        public string contactNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}