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
            CreateMap<CreateFile, File>();
            CreateMap<UpdateFile, File>();
        }
    }
}
