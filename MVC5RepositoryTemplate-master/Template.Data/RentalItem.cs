using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Template.Data
{
    public class RentalItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentaItemId { get; set; }

        public string rentalId { get; set; }
       

        public string ItemCode { get; set; }
        public virtual Item Item { get; set; }

        public int Quantity { get; set; }

        public double price { get; set; }


        public virtual Rental Rental { get; set; }
    }
 
}