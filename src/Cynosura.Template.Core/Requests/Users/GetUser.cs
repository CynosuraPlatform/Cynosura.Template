using System;
using MediatR;
using Cynosura.Template.Core.Requests.Users.Models;

namespace Cynosura.Template.Core.Requests.Users
{
    public class GetUser : IRequest<UserModel>
    {
        public int Id { get; set; }
    }
}
