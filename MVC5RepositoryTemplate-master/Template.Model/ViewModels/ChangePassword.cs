using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Model.ViewModels
{
   public class ChangePasswordViewModel
    {

       [Required(ErrorMessage = "Enter your Current Password")]
       [DataType(DataType.EmailAddress)]
       [Display(Name = "Current Password")]
       public string OldPassword { get; set; }

       [Required(ErrorMessage = "Enter your New Password")]
       [DataType(DataType.EmailAddress)]
       [Display(Name = "New Password")]
       public string NewPassword { get; set; }

       [Required]
       [DataType(DataType.EmailAddress)]
       [Display(Name = "Confirm New Password")]
       [Compare("NewPassword", ErrorMessage = "The New Password must match the Confirmation Password")]
       public string ConfirmNewPassword { get; set; }



       
    }
}
