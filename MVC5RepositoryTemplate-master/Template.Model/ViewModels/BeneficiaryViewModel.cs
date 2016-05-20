using System.ComponentModel.DataAnnotations;

namespace Template.Model.ViewModels
{
    public class BeneficiaryViewModel
    {
        [Required(ErrorMessage ="Required")]
        [Display(Name = "ID Number")]
        [RegularExpression(@"^([0-9]){2}([0-1][0-9])([0-3][0-9])([0-9]){4}([0-1])([0-9]){2}?$", ErrorMessage = "Not yet valid.")]
        [StringLength(13, ErrorMessage = "SA ID Number must be exactly 13 digits long", MinimumLength = 13)]
        public string benIDNumber { get; set; }//beneficiary IDNumber

        // public string IDNumber { get; set; }//applicant IDNumber - foreign Key

        [Required(ErrorMessage = "Required")]
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
    }
}
