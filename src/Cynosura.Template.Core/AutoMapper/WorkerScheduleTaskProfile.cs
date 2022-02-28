using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.WorkerScheduleTasks;
using Cynosura.Template.Core.Requests.WorkerScheduleTasks.Models;

namespace Cynosura.Template.Core.AutoMapper
{
    public class WorkerScheduleTaskProfile : Profile
    {
        public WorkerScheduleTaskProfile()
        {
            CreateMap<WorkerScheduleTask, WorkerScheduleTaskModel>();
            CreateMap<WorkerScheduleTask, WorkerScheduleTaskShortModel>();
            CreateMap<CreateWorkerScheduleTask, WorkerScheduleTask>();
            CreateMap<UpdateWorkerScheduleTask, WorkerScheduleTask>();
        }
    }
}
