using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Cynosura.Template.Core.Requests.Users
{
    public class CreateUser : IRequest<int>
    {
        [Required]
        [EmailAddress(ErrorMessage = "{0} has invalid format")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must contain between {2} and {1} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public IList<int> RoleIds { get; } = new List<int>();
    }
}
