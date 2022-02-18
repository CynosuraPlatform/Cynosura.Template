using MediatR;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.WorkerRuns.Models;

namespace Cynosura.Template.Core.Requests.WorkerRuns
{
    public class GetWorkerRuns : IRequest<PageModel<WorkerRunModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public WorkerRunFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
