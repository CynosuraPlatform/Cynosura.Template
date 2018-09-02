using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Services.Models;
using Cynosura.Template.Web.Models.RoleViewModels;

namespace Cynosura.Template.Web.Infrastructure.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleUpdateViewModel, RoleUpdateModel>();
            CreateMap<RoleCreateViewModel, RoleCreateModel>();
        }
    }
}
