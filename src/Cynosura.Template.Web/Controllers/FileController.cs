using System;
using System.Threading.Tasks;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Files;
using Cynosura.Template.Core.Requests.Files.Models;
using Cynosura.Template.Web.Models;
using Cynosura.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cynosura.Template.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadFile")]
    [ValidateModel]
    [Route("api")]
    public class FileController : Controller
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetFiles")]
        public async Task<PageModel<FileModel>> GetFilesAsync([FromBody] GetFiles getFiles)
        {
            return await _mediator.Send(getFiles);
        }

        [HttpPost("GetFile")]
        public async Task<FileModel> GetFileAsync([FromBody] GetFile getFile)
        {
            return await _mediator.Send(getFile);
        }

        [HttpPost("ExportFiles")]
        public async Task<FileResult> ExportFilesAsync([FromBody] ExportFiles exportFiles)
        {
            var file = await _mediator.Send(exportFiles);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteFile")]
        [HttpPost("UpdateFile")]
        public async Task<Unit> UpdateFileAsync([FromBody] UpdateFile updateFile)
        {
            return await _mediator.Send(updateFile);
        }

        [Authorize("WriteFile")]
        [HttpPost("CreateFile")]
        public async Task<CreatedEntity<int>> CreateFileAsync([FromBody] CreateFile createFile)
        {
            return await _mediator.Send(createFile);
        }

        [Authorize("WriteFile")]
        [HttpPost("DeleteFile")]
        public async Task<Unit> DeleteFileAsync([FromBody] DeleteFile deleteFile)
        {
            return await _mediator.Send(deleteFile);
        }
    }
}