using System;
using System.Collections.Generic;

namespace Cynosura.Template.Core.Requests.Users.Models
{
    public class UserShortModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public override string ToString()
        {
            return $"{UserName}";
        }
    }
}
