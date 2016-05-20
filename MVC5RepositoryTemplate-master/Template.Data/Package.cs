using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Data
{
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int packageID { get; set; }
        public string Name { get; set; }      
        public double PremiumAmount { get; set; }
        public int maxBeneficiary { get; set; }
        public virtual PolicyHolder PolicyHolders { get; set; }        
    }
    public class PackageBenefit
    {
        [Key]
        public int pbID { get; set; }
        public int packageID { get; set; }
        public int benefitID { get; set; }
        public virtual ICollection<Benefit> Benefits { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }
    public class Benefit
    {
        [Key]
        public int benefitID { get; set; }
        public string Name { get; set; }
    }
}
