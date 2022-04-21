using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerceProjectMVC.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage ="Address Required")]
        [MaxLength(50,ErrorMessage = "Address Must Be Less Than 50 Letter")]
        [MinLength(3,ErrorMessage = "Address Must Be Greater Than 3 Letter")]
        public string Address { get; set; }
        public double TotalPrice { get; set; }


        [Required(ErrorMessage ="Status Required")]
        [RegularExpression("^(Pending|Cancel|Compelete)$")]
        public string Name { get; set; }

        public string Status { get; set; }
        public string Country { get; set; }
        public string District { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Required(ErrorMessage = "Date Required")]

        public DateTime Date { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; } 
        
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<ProductOrder> ProductOrders { get; set; }


    }
}
