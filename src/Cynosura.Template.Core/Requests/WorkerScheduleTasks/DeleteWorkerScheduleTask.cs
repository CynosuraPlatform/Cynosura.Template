using System;
using MediatR;

namespace Cynosura.Template.Core.Requests.WorkerScheduleTasks
{
    public class DeleteWorkerScheduleTask : IRequest
    {
        public int Id { get; set; }
    }
}
