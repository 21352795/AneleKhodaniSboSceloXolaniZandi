using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Template.Data;

namespace Template.Model.ViewModels
{
    public class ApplicationViewModel
    {
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
        public string packageID { get; set; }
        //application info
        [Display(Name = "Date Recieved")]
        public DateTime dateSubmitted { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }

        public List<ClientApplicationBeneficiary> beneficiaries { get; set; }
        public List<ClientApplicationDocument> documents { get; set; }
    }
    public class ProfileApplicationView
    {
        [Key]
        public int beneficiaryID { get; set; }
        //policy holder information
       
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        [Display(Name = "Applied to")]
        public string AddOrDelete { get; set; }

        //beneficiary information
        [Display(Name = "ID Number")]
        public string benIDNumber { get; set; }//beneficiary IDNumber
        [Display(Name = "First Name")]
        public string benfirstName { get; set; }
        [Display(Name = "Last Name")]
        public string benlastName { get; set; }
        [Display(Name = "Relationship")]
        public string relationship { get; set; }
        [Display(Name = "Age")]
        public int age { get; set; }
        [Display(Name = "Reason")]
        public string reason { get; set; }

        public List<ProfileApplicationDocuments> documents { get; set; }
    }
}
