using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cynosura.Template.Web.Models.UserViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "{0} has invalid format")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required]
        [StringLength(100, ErrorMessage = "{0} must contain between {2} and {1} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public IEnumerable<int> RoleIds { get; set; }
    }
}
