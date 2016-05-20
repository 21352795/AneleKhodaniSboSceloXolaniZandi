using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Data
{
    public class PolicyHolder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int policyNo { get; set; }
        //personal information
        public string IDNumber { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string province { get; set; }
        //contact information
        public string contactNumber { get; set; }
        public string emailAdress { get; set; }
        public string physicalAddress { get; set; }
        public string postalAddress { get; set; }
        //package info
        //[ForeignKey("packageID")]
        public int packageID { get; set; }
        //public virtual Package Packages { get; set; }
        //membership info
        public DateTime dateStarted { get; set; }
        public string status { get; set; }

        //Relational entities
        public ICollection<PolicyBeneficiary> PolicyBeneficiaries { get; set; }
        public ICollection<PolicyDocument> PolicyDocuments { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }
}
