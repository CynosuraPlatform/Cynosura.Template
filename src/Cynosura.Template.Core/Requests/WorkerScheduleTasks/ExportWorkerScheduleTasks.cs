using MediatR;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.WorkerScheduleTasks.Models;

namespace Cynosura.Template.Core.Requests.WorkerScheduleTasks
{
    public class ExportWorkerScheduleTasks : IRequest<FileContentModel>
    {
        public WorkerScheduleTaskFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
