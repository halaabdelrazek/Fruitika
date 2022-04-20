using System.ComponentModel.DataAnnotations;

namespace ecommerceProjectMVC.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
