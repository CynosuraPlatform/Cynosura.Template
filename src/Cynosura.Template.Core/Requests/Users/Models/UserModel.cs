﻿using System.Collections.Generic;

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
