using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ecommerceProjectMVC.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; } 

        public virtual List<Product> Products { get; set; }
    }
}
