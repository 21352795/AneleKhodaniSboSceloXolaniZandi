using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Data
{
    public class ClientApplicationDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int documentID { get; set; }
        public int applicationID { get; set; }//Foreign Key references ClientApplication
        public string IDNumber { get; set; }//ID Number of the document owner/ document for
        public string fullname { get; set; }
        public string documentName { get; set; }
        public byte[] document { get; set; }
        public virtual ClientApplication ClientApplications { get; set; }
    }
}
