using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Services.Models;

namespace Cynosura.Template.Core.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateModel, User>();
            CreateMap<UserUpdateModel, User>();
        }
    }
}
