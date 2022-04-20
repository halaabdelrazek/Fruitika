using System.ComponentModel.DataAnnotations;

namespace ecommerceProjectMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name ="Name")]
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
        [Required]

        public string PhoneNumber { get; set; }


        public string Image { get; set; }

    }
}
