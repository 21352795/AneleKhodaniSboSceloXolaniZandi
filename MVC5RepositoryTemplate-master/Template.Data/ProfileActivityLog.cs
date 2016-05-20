using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Data
{
    public class ProfileActivityLog
    {
        [Key]
        public int eventID { get; set; }
        public string IDNumber { get; set; }
        public DateTime EventDate { get; set; }
        public string Activity { get; set; }
    }
}
