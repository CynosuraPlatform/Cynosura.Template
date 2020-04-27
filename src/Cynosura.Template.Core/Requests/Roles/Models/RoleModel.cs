using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cynosura.Template.Core.Requests.Roles.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}
