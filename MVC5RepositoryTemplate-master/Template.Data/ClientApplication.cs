using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Data
{
    public class ClientApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int applicationID { get; set; }
        //personal information
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        public string province { get; set; }
        //contact information
        [Display(Name = "Contact Number")]
        public string contactNumber { get; set; }
        [Display(Name = "Email")]
        public string emailAdress { get; set; }
        public string physicalAddress { get; set; }
        public string postalAddress { get; set; }
        //package info
        [Display(Name = "Package")]
        public int packageID { get; set; }
        //application info
        [Display(Name = "Date Recieved")]
        public DateTime dateSubmitted { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }

        //Relational entities
        public ICollection<ClientApplicationBeneficiary> ClientApplicationBeneficiaries { get; set; }
        public ICollection<ClientApplicationDocument> ClientApplicationDocuments { get; set; }
    }
}
