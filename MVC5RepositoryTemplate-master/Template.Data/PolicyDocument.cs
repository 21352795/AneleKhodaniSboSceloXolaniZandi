using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Data
{
    public class PolicyDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int documentID { get; set; }
        public int policyNo { get; set; }//Foreign Key references PolicyHolder 
        public string IDNumber { get; set; }//ID Number of the document owner/ document for
        public string fullname { get; set; }
        public string documentName { get; set; }
        public byte[] document { get; set; }
        public virtual PolicyHolder PolicyHolder { get; set; }
    }
}
