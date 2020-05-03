using System;
using Cynosura.Template.Core.Infrastructure;

namespace Cynosura.Template.Core.Requests.Users.Models
{
    public class UserFilter : EntityFilter
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
