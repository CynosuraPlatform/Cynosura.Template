using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.FileGroups;
using Cynosura.Template.Core.Requests.FileGroups.Models;

namespace Cynosura.Template.Core.AutoMapper
{
    public class FileGroupProfile : Profile
    {
        public FileGroupProfile()
        {
            CreateMap<FileGroup, FileGroupModel>();
            CreateMap<FileGroup, FileGroupShortModel>();
            CreateMap<CreateFileGroup, FileGroup>();
            CreateMap<UpdateFileGroup, FileGroup>();
        }
    }
}
