using System;
using System.Collections.Generic;
using System.Text;
using Cynosura.Template.Core.Infrastructure;
using MediatR;

namespace Cynosura.Template.Core.Requests.Account
{
    public class Register : IRequest<CreatedEntity<int>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
