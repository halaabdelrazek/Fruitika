using System.ComponentModel.DataAnnotations;

namespace ecommerceProjectMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }

        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Address { get; set; }


        public string Image { get; set; }

    }
}
