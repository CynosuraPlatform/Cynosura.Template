using MediatR;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.FileGroups.Models;

namespace Cynosura.Template.Core.Requests.FileGroups
{
    public class GetFileGroups : IRequest<PageModel<FileGroupModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public FileGroupFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
