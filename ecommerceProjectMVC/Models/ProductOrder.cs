using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceProjectMVC.Models
{
    public class ProductOrder
    {
        public int ProductOrderId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }    

        public virtual Order Order { get; set; }


        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }




    }
}
