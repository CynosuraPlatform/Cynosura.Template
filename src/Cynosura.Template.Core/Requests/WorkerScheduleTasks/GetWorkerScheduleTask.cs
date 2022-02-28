using System;
using MediatR;
using Cynosura.Template.Core.Requests.WorkerScheduleTasks.Models;

namespace Cynosura.Template.Core.Requests.WorkerScheduleTasks
{
    public class GetWorkerScheduleTask : IRequest<WorkerScheduleTaskModel?>
    {
        public int Id { get; set; }
    }
}
