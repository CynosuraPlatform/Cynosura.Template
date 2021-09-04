using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.WorkerRuns;
using Cynosura.Template.Core.Requests.WorkerRuns.Models;

namespace Cynosura.Template.Core.AutoMapper
{
    public class WorkerRunProfile : Profile
    {
        public WorkerRunProfile()
        {
            CreateMap<WorkerRun, WorkerRunModel>();
            CreateMap<WorkerRun, WorkerRunShortModel>();
            CreateMap<CreateWorkerRun, WorkerRun>();
        }
    }
}
