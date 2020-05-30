using MediatR;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.FileGroups.Models;

namespace Cynosura.Template.Core.Requests.FileGroups
{
    public class ExportFileGroups : IRequest<FileContentModel>
    {
        public FileGroupFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
