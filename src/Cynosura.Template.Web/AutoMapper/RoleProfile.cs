using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Requests.Roles;
using Cynosura.Template.Core.Requests.Roles.Models;
using Cynosura.Template.Web.Protos.Roles;

namespace Cynosura.Template.Web.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateRoleRequest, CreateRole>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateRoleRequest.NameOneOfOneofCase.Name));
            CreateMap<DeleteRoleRequest, DeleteRole>();
            CreateMap<GetRoleRequest, GetRole>();
            CreateMap<GetRolesRequest, GetRoles>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetRolesRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetRolesRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetRolesRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateRoleRequest, UpdateRole>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateRoleRequest.NameOneOfOneofCase.Name));

            CreateMap<RoleModel, Role>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default));
            CreateMap<PageModel<RoleModel>, RolePageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Role>>(src.PageItems)));
        }
    }
}
