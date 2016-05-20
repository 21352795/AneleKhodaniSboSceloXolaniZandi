using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Data
{
    public class ArchivedClientApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int applicationID { get; set; }
        //personal information
        public string IDNumber { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string nationality { get; set; }
        public string province { get; set; }
        //contact information
        public string contactNumber { get; set; }
        public string emailAdress { get; set; }
        public string physicalAddress { get; set; }
        public string postalAddress { get; set; }
        //Archive info
        public DateTime dateArchived { get; set; }
        public string reason { get; set; }

        //Relational entities
    }
}
