using System.ComponentModel.DataAnnotations;

namespace Template.Model.RegisterNlogin
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter your ID Number")]
        [Display(Name = "ID Number")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
}