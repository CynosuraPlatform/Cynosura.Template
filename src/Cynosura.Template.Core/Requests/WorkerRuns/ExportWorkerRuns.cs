using MediatR;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.WorkerRuns.Models;

namespace Cynosura.Template.Core.Requests.WorkerRuns
{
    public class ExportWorkerRuns : IRequest<FileContentModel>
    {
        public WorkerRunFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
