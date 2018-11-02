using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.Roles;
using Cynosura.Template.Core.Requests.Roles.Models;

namespace Cynosura.Template.Core.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleModel>();
            CreateMap<CreateRole, Role>();
            CreateMap<UpdateRole, Role>();
        }
    }
}
