using System.Collections.Generic;

namespace Cynosura.Template.Core.Services.Models
{
    public class UserCreateModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IEnumerable<int> RoleIds { get; set; }
    }
}
