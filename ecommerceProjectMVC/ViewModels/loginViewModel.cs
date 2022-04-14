using System.ComponentModel.DataAnnotations;

namespace ecommerceProjectMVC.ViewModels
{
    public class loginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
