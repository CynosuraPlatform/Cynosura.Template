using System;
using System.Collections.Generic;
using System.Text;
using Cynosura.Template.Core.Requests.Users.Models;
using MediatR;

namespace Cynosura.Template.Core.Requests.Users
{
    public class GetUser : IRequest<UserModel>
    {
        public int Id { get; set; }
    }
}
