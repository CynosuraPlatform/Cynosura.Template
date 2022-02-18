using MediatR;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Files.Models;

namespace Cynosura.Template.Core.Requests.Files
{
    public class ExportFiles : IRequest<FileContentModel>
    {
        public FileFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
