using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Cynosura.Template.Core.Requests.Users
{
    public class DeleteUser : IRequest
    {
        public int Id { get; set; }
    }
}
