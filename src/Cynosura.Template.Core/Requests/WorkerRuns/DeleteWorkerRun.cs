using System;
using MediatR;

namespace Cynosura.Template.Core.Requests.WorkerRuns
{
    public class DeleteWorkerRun : IRequest
    {
        public int Id { get; set; }
    }
}
