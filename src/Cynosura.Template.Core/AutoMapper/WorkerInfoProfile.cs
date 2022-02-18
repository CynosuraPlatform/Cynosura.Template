using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.WorkerInfos;
using Cynosura.Template.Core.Requests.WorkerInfos.Models;

namespace Cynosura.Template.Core.AutoMapper
{
    public class WorkerInfoProfile : Profile
    {
        public WorkerInfoProfile()
        {
            CreateMap<WorkerInfo, WorkerInfoModel>();
            CreateMap<WorkerInfo, WorkerInfoShortModel>();
            CreateMap<CreateWorkerInfo, WorkerInfo>();
            CreateMap<UpdateWorkerInfo, WorkerInfo>();
        }
    }
}
