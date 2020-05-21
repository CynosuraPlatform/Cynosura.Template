using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.FileGroups.Models;
using MediatR;

namespace Cynosura.Template.Core.Requests.FileGroups
{
    public class ExportFileGroups : IRequest<FileModel>
    {
        public FileGroupFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
