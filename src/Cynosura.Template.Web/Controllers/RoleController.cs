using System.Threading.Tasks;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Roles;
using Cynosura.Template.Core.Requests.Roles.Models;
using Cynosura.Template.Web.Models;
using Cynosura.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cynosura.Template.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize(Roles = "Administrator")]
    [ValidateModel]
    [Route("api")]
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetRoles")]
        public async Task<PageModel<RoleModel>> GetRolesAsync([FromBody] GetRoles getRoles)
        {
            return await _mediator.Send(getRoles);
        }

        [HttpPost("GetRole")]
        public async Task<RoleModel> GetRoleAsync([FromBody] GetRole getRole)
        {
            return await _mediator.Send(getRole);
        }

        [HttpPost("UpdateRole")]
        public async Task<Unit> UpdateRoleAsync([FromBody] UpdateRole updateRole)
        {
            return await _mediator.Send(updateRole);
        }

        [HttpPost("CreateRole")]
        public async Task<CreatedEntity<int>> PostRoleAsync([FromBody] CreateRole createRole)
        {
            return await _mediator.Send(createRole);
        }

        [HttpPost("DeleteRole")]
        public async Task<Unit> DeleteRoleAsync([FromBody] DeleteRole deleteRole)
        {
            return await _mediator.Send(deleteRole);
        }
    }
}
