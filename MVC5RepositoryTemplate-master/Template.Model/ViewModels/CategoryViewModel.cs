using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Template.Data;

namespace Template.Model.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string  Description { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
   
}