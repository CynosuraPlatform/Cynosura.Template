using MediatR;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.WorkerInfos.Models;

namespace Cynosura.Template.Core.Requests.WorkerInfos
{
    public class ExportWorkerInfos : IRequest<FileContentModel>
    {
        public WorkerInfoFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
