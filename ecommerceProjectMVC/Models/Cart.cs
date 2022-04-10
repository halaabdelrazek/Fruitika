using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceProjectMVC.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
