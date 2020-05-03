using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MediatR;

namespace Cynosura.Template.Core.Requests.Profile
{
    public class UpdateProfile : IRequest
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}
