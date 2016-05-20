using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Data
{
    public class ProfileApplicationBeneficiary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int beneficiaryID { get; set; }
        //personal information
        public string benIDNumber { get; set; }//beneficiary IDNumber
        public string IDNumber { get; set; }//applicant IDNumber - foreign Key
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string relationship { get; set; }
        public int age { get; set; }
        public string AddOrDelete { get; set; }
        public string reason { get; set; }
        //relational entities
    }
}
