using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Template.Model.ViewModels
{
    public class PackageView
    {
        [Key]
        [Display(Name = "Package ID")]
        public int PackageId { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Name doesn't meet recommended length", MinimumLength = 3)]
        [Display(Name= "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Premium Amount p/Month")]
        [DataType(DataType.Currency)]
        public double PremiumAmount { get; set; }
        [Required]
        [Display(Name = "Max No. of Beneficiaries")]
        [Range(1,10,ErrorMessage ="This field must be at least one and cannot exceed 10.")]
        public int maxBeneficiary { get; set; }
    }
    public class PackageBenefitView
    {
        [Key]
        [Display(Name = "ID")]
        public int pbID { get; set; }

        [Display(Name = "Package Name")]
        public string Name { get; set; }
        [Display(Name = "Package ID")]
        public int packageID { get; set; }
        [Display(Name = "Premium Amount p/Month")]
        [DataType(DataType.Currency)]
        public double PremiumAmount { get; set; }
        [Display(Name = "Max No. of Beneficiaries")]        
        public int maxBeneficiary { get; set; }
        [Display(Name = "Package Benefits")]
        public List<BenefitView> benefit { get; set; }
    }
    public class BenefitView
    {
        [Key]
        [Display(Name = "Benefit ID")]
        public int benefitID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [StringLength(30, ErrorMessage = "Name doesn't meet recommended length", MinimumLength = 3)]
        public string Name { get; set; }
        public bool isBenefitSelected { get; set; }
    }
}
