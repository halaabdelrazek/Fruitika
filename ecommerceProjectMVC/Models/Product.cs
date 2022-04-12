using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceProjectMVC.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public double Price { get; set; }


        [ForeignKey("Category")]

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Cart> Carts { get; set; }

        public virtual List<ProductOrder> ProductOrders { get; set; }


    }
}
