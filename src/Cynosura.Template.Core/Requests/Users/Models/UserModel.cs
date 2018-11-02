using System;
using System.Collections.Generic;
using System.Text;

namespace Cynosura.Template.Core.Requests.Users.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IList<int> RoleIds { get; } = new List<int>();
    }
}
