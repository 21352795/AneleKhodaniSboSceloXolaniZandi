using System.ComponentModel.DataAnnotations;

namespace Template.Model.ViewModels
{
    public class DocumentViewModel
    {
        public int documentID { get; set; }
        public int applicationID { get; set; }//Foreign Key references ClientApplication
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }
        [Display(Name = "Full Name")]
        public string fullname { get; set; }

        [Required]
        [Display(Name = "Required Doc")]
        public string documentName { get; set; }
        [Display(Name = "Document")]
        public byte[] document { get; set; }
    }
}
