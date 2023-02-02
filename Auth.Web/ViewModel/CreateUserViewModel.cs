using System.ComponentModel.DataAnnotations;

namespace Auth.Web.ViewModel
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="User Email")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "User PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "User Password")]
        public string Password { get; set; }

    }
}
