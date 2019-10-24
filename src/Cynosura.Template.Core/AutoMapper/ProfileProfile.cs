using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.Profile.Models;

namespace Cynosura.Template.Core.AutoMapper
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            CreateMap<User, ProfileModel>();
        }
    }
}
