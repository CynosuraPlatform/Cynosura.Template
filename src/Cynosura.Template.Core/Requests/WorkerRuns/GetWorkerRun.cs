using System;
using MediatR;
using Cynosura.Template.Core.Requests.WorkerRuns.Models;

namespace Cynosura.Template.Core.Requests.WorkerRuns
{
    public class GetWorkerRun : IRequest<WorkerRunModel?>
    {
        public int Id { get; set; }
    }
}
