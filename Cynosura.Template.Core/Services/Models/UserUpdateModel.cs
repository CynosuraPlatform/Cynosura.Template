using System.Collections.Generic;

namespace Cynosura.Template.Core.Services.Models
{
    public class UserUpdateModel
    {
        public string NewPassword { get; set; }
        public IEnumerable<int> RoleIds { get; set; }
    }
}
