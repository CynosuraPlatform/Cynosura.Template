using MediatR;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Roles.Models;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class GetRoles : IRequest<PageModel<RoleModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public RoleFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
