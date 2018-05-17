using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cynosura.Template.Web.Models.UserViewModels
{
    public class UpdateUserViewModel
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "{0} must contain between {2} and {1} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmNewPassword { get; set; }
        public IEnumerable<int> RoleIds { get; set; }
    }
}
