using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Services.Models;
using Cynosura.Template.Web.Models.UserViewModels;

namespace Cynosura.Template.Web.Infrastructure.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<CreateUserViewModel, UserCreateModel>();
            CreateMap<UpdateUserViewModel, UserUpdateModel>();
        }
    }
}
