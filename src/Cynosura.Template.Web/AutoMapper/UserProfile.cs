using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Requests.Users;
using Cynosura.Template.Core.Requests.Users.Models;
using Cynosura.Template.Web.Protos.Users;

namespace Cynosura.Template.Web.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequest, CreateUser>();
            CreateMap<DeleteUserRequest, DeleteUser>();
            CreateMap<GetUserRequest, GetUser>();
            CreateMap<GetUsersRequest, GetUsers>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetUsersRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetUsersRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetUsersRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateUserRequest, UpdateUser>();

            CreateMap<UserModel, User>()
                .ForMember(dest => dest.Email, opt => opt.NullSubstitute(""));
            CreateMap<PageModel<UserModel>, UserPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<User>>(src.PageItems)));
        }
    }
}
