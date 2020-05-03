using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Cynosura.Template.Core.Requests.Users
{
    public class UpdateUser : IRequest
    {
        public int Id { get; set; }

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
