using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Data
{
    public class ArchiveServiceRepresentative
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ServiceRepIdNo { get; set; }
        public string IDNumber { get; set; }
        public string Fullname { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string AppUserId { get; set; }
    }
}
