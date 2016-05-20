using System.ComponentModel.DataAnnotations;
using Template.Data;

namespace Template.Model.ViewModels
{
    public class RentalItemViewModel
    {
        
     
        public int RentaItemId { get; set; }

        public string rentalId { get; set; }

        public string ItemCode { get; set; }
        public virtual Item Item { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        public virtual Rental Rental { get; set; }
    }
 
}