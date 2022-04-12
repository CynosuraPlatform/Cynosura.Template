using System.Linq;
using AutoMapper;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.Users;
using Cynosura.Template.Core.Requests.Users.Models;

namespace Cynosura.Template.Core.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(dst => dst.RoleIds, opt => opt.MapFrom(src => src.Roles.Select(e => e.Id)));
            CreateMap<User, UserShortModel>();
            CreateMap<CreateUser, User>();
            CreateMap<UpdateUser, User>();
        }
    }
}
