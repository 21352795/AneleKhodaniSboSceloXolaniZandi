using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Model.ViewModels
{
    public class PolicyHolderViewModel
    {
        [Key]
        [Display(Name = "Policy No.")]
        public int policyNo { get; set; }
        //personal information
        [Display(Name ="ID Number")]
        public string IDNumber { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        
        //membership info
        [Display(Name = "Member since")]
        public DateTime dateStarted { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
    }
    public class PolicyHolderView
    {
        public PersonalInfoViewModel personal { get; set; }
        public ContactDetailsViewModel contact { get; set; }
        public PolicyDetailsViewModel policy { get; set; }
        public List<BeneficiariesViewModel> beneficiaries { get; set; }
    }
}
