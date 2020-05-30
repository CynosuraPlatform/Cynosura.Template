using MediatR;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Users.Models;

namespace Cynosura.Template.Core.Requests.Users
{
    public class ExportUsers : IRequest<FileContentModel>
    {
        public UserFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
