﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Users;
using Cynosura.Template.Core.Requests.Users.Models;

namespace Cynosura.Template.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadUser")]
    [ValidateModel]
    [Route("api")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetUsers")]
        public async Task<PageModel<UserModel>> GetUsersAsync([FromBody] GetUsers getUsers)
        {
            return await _mediator.Send(getUsers);
        }

        [HttpPost("GetUser")]
        public async Task<UserModel?> GetUserAsync([FromBody] GetUser getUser)
        {
            return await _mediator.Send(getUser);
        }

        [HttpPost("ExportUsers")]
        public async Task<FileResult> ExportUsersAsync([FromBody] ExportUsers exportUsers)
        {
            var file = await _mediator.Send(exportUsers);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteUser")]
        [HttpPost("UpdateUser")]
        public async Task UpdateUserAsync([FromBody] UpdateUser updateUser)
        {
            await _mediator.Send(updateUser);
        }

        [Authorize("WriteUser")]
        [HttpPost("CreateUser")]
        public async Task<CreatedEntity<int>> CreateUserAsync([FromBody] CreateUser createUser)
        {
            return await _mediator.Send(createUser);
        }

        [Authorize("WriteUser")]
        [HttpPost("DeleteUser")]
        public async Task DeleteUserAsync([FromBody] DeleteUser deleteUser)
        {
            await _mediator.Send(deleteUser);
        }
    }
}
