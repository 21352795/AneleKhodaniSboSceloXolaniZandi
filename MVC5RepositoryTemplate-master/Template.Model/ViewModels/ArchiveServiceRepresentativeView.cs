using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Model.RegisterNlogin;

namespace Template.Model.ViewModels
{
    public class ArchiveServiceRepresentativeView
    {
        [Key]
        [DisplayName("Service Representative Number")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ServiceRepIdNo { get; set; }

        [Required(ErrorMessage = "Enter ID Number")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Your ID Number Should Be  a length of 13 and not exceed ")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid ID Number")]
        [DisplayName("ID Number")]
        public string IDNumber { get; set; }

        [Required(ErrorMessage = "Please enter your full name")]
        [DisplayName("Full Name")]
        [RegularExpression(@"^[a-zA-Z]* {0,1}[a-zA-Z]* {0,1}[a-zA-Z]*$", ErrorMessage = "Use letters only please")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Please enter your contact number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Use Numbers only please")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Your Contact Number Should Be a length of 10 digits and not exceed ")]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Contact Number")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string Email { get; set; }
        public RegisterModel User { get; set; }
    }
}
