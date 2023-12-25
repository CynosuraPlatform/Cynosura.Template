﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Users;
using Cynosura.Template.Core.Requests.Users.Models;
using Cynosura.Template.Web.Protos;
using Cynosura.Template.Web.Protos.Users;

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
            var model = await _mediator.Send(getUsers);
            return _mapper.Map<PageModel<UserModel>, UserPageModel>(model);
        }

        public override async Task<User> GetUser(GetUserRequest getUserRequest, ServerCallContext context)
        {
            var getUser = _mapper.Map<GetUserRequest, GetUser>(getUserRequest);
            var model = await _mediator.Send(getUser);
            return _mapper.Map<UserModel, User>(model!);
        }

        [Authorize("WriteUser")]
        public override async Task<Empty> UpdateUser(UpdateUserRequest updateUserRequest, ServerCallContext context)
        {
            var updateUser = _mapper.Map<UpdateUserRequest, UpdateUser>(updateUserRequest);
            await _mediator.Send(updateUser);
            return new Empty();
        }

        [Authorize("WriteUser")]
        public override async Task<CreatedEntity> CreateUser(CreateUserRequest createUserRequest, ServerCallContext context)
        {
            var createUser = _mapper.Map<CreateUserRequest, CreateUser>(createUserRequest);
            var model = await _mediator.Send(createUser);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteUser")]
        public override async Task<Empty> DeleteUser(DeleteUserRequest deleteUserRequest, ServerCallContext context)
        {
            var deleteUser = _mapper.Map<DeleteUserRequest, DeleteUser>(deleteUserRequest);
            await _mediator.Send(deleteUser);
            return new Empty();
        }
    }
}
