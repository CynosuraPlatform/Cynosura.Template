using System;
using MediatR;
using Cynosura.Template.Core.Requests.Roles.Models;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class GetRole : IRequest<RoleModel>
    {
        public int Id { get; set; }
    }
}
