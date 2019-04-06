using System.Threading.Tasks;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Users;
using Cynosura.Template.Core.Requests.Users.Models;
using Cynosura.Template.Web.Models;
using Cynosura.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cynosura.Template.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize(Roles = "Administrator")]
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
        public async Task<UserModel> GetUserAsync([FromBody] GetUser getUser)
        {
            return await _mediator.Send(getUser);
        }

        [HttpPost("UpdateUser")]
        public async Task<Unit> UpdateUserAsync([FromBody] UpdateUser updateUser)
        {
            return await _mediator.Send(updateUser);
        }

        [HttpPost("CreateUser")]
        public async Task<CreatedEntity<int>> CreateUserAsync([FromBody] CreateUser createUser)
        {
            return await _mediator.Send(createUser);
        }

        [HttpPost("DeleteUser")]
        public async Task<Unit> DeleteUserAsync([FromBody] DeleteUser deleteUser)
        {
            return await _mediator.Send(deleteUser);
        }
    }
}