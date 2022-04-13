using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceProjectMVC.Models
{
    public class ProductOrder
    {
        
        public int ProductOrderId { get; set; }

        [Required]
        [MinLength(1)]
        public int Quantity { get; set; }
        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }    

        public virtual Order Order { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }




    }
}
