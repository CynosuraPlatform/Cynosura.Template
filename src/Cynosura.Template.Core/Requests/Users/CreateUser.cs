using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cynosura.Template.Core.Infrastructure;
using MediatR;

namespace Cynosura.Template.Core.Requests.Users
{
    public class CreateUser : IRequest<CreatedEntity<int>>
    {
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public List<int> RoleIds { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}
