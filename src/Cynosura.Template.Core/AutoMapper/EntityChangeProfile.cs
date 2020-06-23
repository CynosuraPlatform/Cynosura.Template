using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.EntityChanges.Models;

namespace Cynosura.Template.Core.AutoMapper
{
    public class EntityChangeProfile : Profile
    {
        public EntityChangeProfile()
        {
            CreateMap<EntityChange, EntityChangeModel>();
        }
    }
}
