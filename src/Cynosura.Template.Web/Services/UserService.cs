﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Users;
using Cynosura.Template.Core.Requests.Users.Models;
using Cynosura.Template.Web.Protos;
using Cynosura.Template.Web.Protos.Users;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Cynosura.Template.Web.Services
{
    [Authorize("ReadUser")]
    public class UserService : Protos.Users.UserService.UserServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<UserPageModel> GetUsers(GetUsersRequest getUsersRequest, ServerCallContext context)
        {
            var getUsers = _mapper.Map<GetUsersRequest, GetUsers>(getUsersRequest);
            return _mapper.Map<PageModel<UserModel>, UserPageModel>(await _mediator.Send(getUsers));
        }

        public override async Task<User> GetUser(GetUserRequest getUserRequest, ServerCallContext context)
        {
            var getUser = _mapper.Map<GetUserRequest, GetUser>(getUserRequest);
            return _mapper.Map<UserModel, User>(await _mediator.Send(getUser));
        }

        [Authorize("WriteUser")]
        public override async Task<Empty> UpdateUser(UpdateUserRequest updateUserRequest, ServerCallContext context)
        {
            var updateUser = _mapper.Map<UpdateUserRequest, UpdateUser>(updateUserRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(updateUser));
        }

        [Authorize("WriteUser")]
        public override async Task<CreatedEntity> CreateUser(CreateUserRequest createUserRequest, ServerCallContext context)
        {
            var createUser = _mapper.Map<CreateUserRequest, CreateUser>(createUserRequest);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(await _mediator.Send(createUser));
        }

        [Authorize("WriteUser")]
        public override async Task<Empty> DeleteUser(DeleteUserRequest deleteUserRequest, ServerCallContext context)
        {
            var deleteUser = _mapper.Map<DeleteUserRequest, DeleteUser>(deleteUserRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(deleteUser));
        }
    }
}
