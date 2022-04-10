using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceProjectMVC.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Cart> Carts { get; set; }

        public virtual List<ProductOrder> ProductOrders { get; set; }







    }
}
