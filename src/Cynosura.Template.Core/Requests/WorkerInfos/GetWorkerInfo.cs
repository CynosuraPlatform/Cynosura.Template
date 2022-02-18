using System;
using MediatR;
using Cynosura.Template.Core.Requests.WorkerInfos.Models;

namespace Cynosura.Template.Core.Requests.WorkerInfos
{
    public class GetWorkerInfo : IRequest<WorkerInfoModel>
    {
        public int Id { get; set; }
    }
}
