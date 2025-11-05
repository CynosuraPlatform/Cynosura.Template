using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Requests.WorkerInfos;
using Cynosura.Template.Core.Requests.WorkerInfos.Models;
using Cynosura.Template.Web.Protos.WorkerInfos;

namespace Cynosura.Template.Web.AutoMapper
{
    public class WorkerInfoProfile : Profile
    {
        public WorkerInfoProfile()
        {
            CreateMap<CreateWorkerInfoRequest, CreateWorkerInfo>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateWorkerInfoRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.ClassName, opt => opt.Condition(src => src.ClassNameOneOfCase == CreateWorkerInfoRequest.ClassNameOneOfOneofCase.ClassName))
                .ForMember(dest => dest.RetryCount, opt => opt.Condition(src => src.RetryCountOneOfCase == CreateWorkerInfoRequest.RetryCountOneOfOneofCase.RetryCount))
                .ForMember(dest => dest.RetryInterval, opt => opt.Condition(src => src.RetryIntervalOneOfCase == CreateWorkerInfoRequest.RetryIntervalOneOfOneofCase.RetryInterval));
            CreateMap<DeleteWorkerInfoRequest, DeleteWorkerInfo>();
            CreateMap<GetWorkerInfoRequest, GetWorkerInfo>();
            CreateMap<GetWorkerInfosRequest, GetWorkerInfos>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetWorkerInfosRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetWorkerInfosRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetWorkerInfosRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateWorkerInfoRequest, UpdateWorkerInfo>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateWorkerInfoRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.ClassName, opt => opt.Condition(src => src.ClassNameOneOfCase == UpdateWorkerInfoRequest.ClassNameOneOfOneofCase.ClassName))
                .ForMember(dest => dest.RetryCount, opt => opt.Condition(src => src.RetryCountOneOfCase == UpdateWorkerInfoRequest.RetryCountOneOfOneofCase.RetryCount))
                .ForMember(dest => dest.RetryInterval, opt => opt.Condition(src => src.RetryIntervalOneOfCase == UpdateWorkerInfoRequest.RetryIntervalOneOfOneofCase.RetryInterval));

            CreateMap<WorkerInfoModel, WorkerInfo>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.ClassName, opt => opt.Condition(src => src.ClassName != default))
                .ForMember(dest => dest.RetryCount, opt => opt.Condition(src => src.RetryCount != default))
                .ForMember(dest => dest.RetryInterval, opt => opt.Condition(src => src.RetryInterval != default));
            CreateMap<PageModel<WorkerInfoModel>, WorkerInfoPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<WorkerInfo>>(src.PageItems)));
        }
    }
}
