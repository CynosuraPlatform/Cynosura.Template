using Cynosura.Template.Core.Requests.Roles.Models;
using MediatR;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class GetRole : IRequest<RoleModel>
    {
        public int Id { get; set; }
    }
}
