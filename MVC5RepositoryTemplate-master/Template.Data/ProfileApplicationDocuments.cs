using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Data
{
    public class ProfileApplicationDocuments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int documentID { get; set; }
        public string PolicyHolderIDN { get; set; }//Foreign Key references
        public string IDNumber { get; set; }//ID Number of the document owner/ document for
        public string fullname { get; set; }
        public string documentName { get; set; }
        public byte[] document { get; set; }
    }
}
