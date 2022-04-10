using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceProjectMVC.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Address { get; set; }

        public double TotalPrice { get; set; }

        public string Status { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; } 
        
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<ProductOrder> ProductOrders { get; set; }


    }
}
