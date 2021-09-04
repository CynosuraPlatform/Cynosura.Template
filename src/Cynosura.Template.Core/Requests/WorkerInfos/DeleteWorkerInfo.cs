using System;
using MediatR;

namespace Cynosura.Template.Core.Requests.WorkerInfos
{
    public class DeleteWorkerInfo : IRequest
    {
        public int Id { get; set; }
    }
}
