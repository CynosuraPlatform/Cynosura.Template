using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Web.Protos;
using MediatR;

namespace Cynosura.Template.Web.AutoMapper
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<Unit, Empty>();
            CreateMap<CreatedEntity<int>, CreatedEntity>();
        }
    }
}
