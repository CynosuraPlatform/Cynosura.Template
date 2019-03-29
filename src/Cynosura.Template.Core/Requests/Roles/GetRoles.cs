using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Requests.Roles.Models;
using MediatR;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class GetRoles : IRequest<PageModel<RoleModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public RoleFilter Filter { get; set; }
    }
}
