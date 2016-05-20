using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Data
{
    public class ClientApplicationBeneficiary
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
        //relational entities
        public virtual ClientApplication ClientApplications { get; set; }
    }
}
