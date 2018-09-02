using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cynosura.Template.Web.Models.UserViewModels
{
    public class UserCreateViewModel : UserUpdateViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "{0} has invalid format")]
        public string Email { get; set; }
    }
}
