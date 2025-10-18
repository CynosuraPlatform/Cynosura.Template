using System;
using System.Collections.Generic;

namespace Cynosura.Template.Core.Requests.Roles.Models
{
    public class RoleShortModel
    {
        public int Id { get; set; }

        public required string DisplayName { get; set; }

        public override string ToString()
        {
            return $"{DisplayName}";
        }
    }
}
