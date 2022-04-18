using System.ComponentModel.DataAnnotations;

namespace ecommerceProjectMVC.ViewModel
{
    public class UserProfileViweModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "You must provide a phone number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\+201|01|00201)[0-2,5]{1}[0-9]{8}", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }


        //[Required]
        public string Image { get; set; }

        [DataType(DataType.Password)]
        //[Required]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        //[Required]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        //[Required]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }




    }
}
