using MediatR;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.WorkerInfos.Models;

namespace Cynosura.Template.Core.Requests.WorkerInfos
{
    public class GetWorkerInfos : IRequest<PageModel<WorkerInfoModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public WorkerInfoFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
