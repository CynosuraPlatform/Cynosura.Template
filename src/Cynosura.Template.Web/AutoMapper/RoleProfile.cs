using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Requests.Roles;
using Cynosura.Template.Core.Requests.Roles.Models;
using Cynosura.Template.Web.Protos;

namespace Cynosura.Template.Web.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<CreateRoleRequest, CreateRole>();
            CreateMap<DeleteRoleRequest, DeleteRole>();
            CreateMap<GetRoleRequest, GetRole>();
            CreateMap<GetRolesRequest, GetRoles>();
            CreateMap<UpdateRoleRequest, UpdateRole>();

            CreateMap<RoleModel, Role>();
            CreateMap<PageModel<RoleModel>, RolePageModel>();
        }
    }
}
