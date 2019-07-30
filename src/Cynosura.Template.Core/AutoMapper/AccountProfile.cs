using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.Account;

namespace Cynosura.Template.Core.AutoMapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Register, User>();
        }
    }
}
