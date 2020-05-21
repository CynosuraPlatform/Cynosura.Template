using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.Files;
using Cynosura.Template.Core.Requests.Files.Models;

namespace Cynosura.Template.Core.AutoMapper
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, FileModel>();
            CreateMap<File, FileShortModel>();
            CreateMap<CreateFile, File>()
                .ForMember(d => d.Content, o => o.Ignore());
            CreateMap<UpdateFile, File>()
                .ForMember(d => d.Content, o => o.Ignore());
        }
    }
}
