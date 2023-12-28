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
using Cynosura.Template.Core.Requests.Files;
using Cynosura.Template.Core.Requests.Files.Models;
using Cynosura.Template.Web.Protos;
using Cynosura.Template.Web.Protos.Files;

namespace Cynosura.Template.Web.Services
{
    [Authorize("ReadFile")]
    public class FileService : Protos.Files.FileService.FileServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FileService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<FilePageModel> GetFiles(GetFilesRequest getFilesRequest, ServerCallContext context)
        {
            var getFiles = _mapper.Map<GetFilesRequest, GetFiles>(getFilesRequest);
            var model = await _mediator.Send(getFiles);
            return _mapper.Map<PageModel<FileModel>, FilePageModel>(model);
        }

        public override async Task<File> GetFile(GetFileRequest getFileRequest, ServerCallContext context)
        {
            var getFile = _mapper.Map<GetFileRequest, GetFile>(getFileRequest);
            var model = await _mediator.Send(getFile);
            return _mapper.Map<FileModel, File>(model!);
        }

        [Authorize("WriteFile")]
        public override async Task<Empty> UpdateFile(UpdateFileRequest updateFileRequest, ServerCallContext context)
        {
            var updateFile = _mapper.Map<UpdateFileRequest, UpdateFile>(updateFileRequest);
            await _mediator.Send(updateFile);
            return new Empty();
        }

        [Authorize("WriteFile")]
        public override async Task<CreatedEntity> CreateFile(CreateFileRequest createFileRequest, ServerCallContext context)
        {
            var createFile = _mapper.Map<CreateFileRequest, CreateFile>(createFileRequest);
            var model = await _mediator.Send(createFile);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteFile")]
        public override async Task<Empty> DeleteFile(DeleteFileRequest deleteFileRequest, ServerCallContext context)
        {
            var deleteFile = _mapper.Map<DeleteFileRequest, DeleteFile>(deleteFileRequest);
            await _mediator.Send(deleteFile);
            return new Empty();
        }
    }
}
