﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.WorkerRuns;
using Cynosura.Template.Core.Requests.WorkerRuns.Models;

namespace Cynosura.Template.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadWorkerRun")]
    [ValidateModel]
    [Route("api")]
    public class WorkerRunController : Controller
    {
        private readonly IMediator _mediator;

        public WorkerRunController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetWorkerRuns")]
        public async Task<PageModel<WorkerRunModel>> GetWorkerRunsAsync([FromBody] GetWorkerRuns getWorkerRuns)
        {
            return await _mediator.Send(getWorkerRuns);
        }

        [HttpPost("GetWorkerRun")]
        public async Task<WorkerRunModel?> GetWorkerRunAsync([FromBody] GetWorkerRun getWorkerRun)
        {
            return await _mediator.Send(getWorkerRun);
        }

        [HttpPost("ExportWorkerRuns")]
        public async Task<FileResult> ExportWorkerRunsAsync([FromBody] ExportWorkerRuns exportWorkerRuns)
        {
            var file = await _mediator.Send(exportWorkerRuns);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteWorkerRun")]
        [HttpPost("CreateWorkerRun")]
        public async Task<CreatedEntity<int>> CreateWorkerRunAsync([FromBody] CreateWorkerRun createWorkerRun)
        {
            return await _mediator.Send(createWorkerRun);
        }

        [Authorize("WriteWorkerRun")]
        [HttpPost("DeleteWorkerRun")]
        public async Task DeleteWorkerRunAsync([FromBody] DeleteWorkerRun deleteWorkerRun)
        {
            await _mediator.Send(deleteWorkerRun);
        }
    }
}
