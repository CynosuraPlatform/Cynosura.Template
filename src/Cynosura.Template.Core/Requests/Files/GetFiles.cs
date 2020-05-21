using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Files.Models;
using MediatR;

namespace Cynosura.Template.Core.Requests.Files
{
    public class GetFiles : IRequest<PageModel<FileModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public FileFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
