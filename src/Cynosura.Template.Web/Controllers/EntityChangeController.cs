using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.EntityChanges;
using Cynosura.Template.Core.Requests.EntityChanges.Models;

namespace Cynosura.Template.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadEntityChange")]
    [ValidateModel]
    [Route("api")]
    public class EntityChangeController : Controller
    {
        private readonly IMediator _mediator;

        public EntityChangeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetEntityChanges")]
        public async Task<PageModel<EntityChangeModel>> GetEntityChangesAsync([FromBody] GetEntityChanges getEntityChanges)
        {
            return await _mediator.Send(getEntityChanges);
        }
    }
}
