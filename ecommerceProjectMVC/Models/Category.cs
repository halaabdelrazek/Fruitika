using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual List<Product> Products { get; set; }
    }
}
