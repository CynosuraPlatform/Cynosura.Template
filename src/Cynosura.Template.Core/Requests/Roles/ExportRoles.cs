using MediatR;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Roles.Models;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class ExportRoles : IRequest<FileContentModel>
    {
        public RoleFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
