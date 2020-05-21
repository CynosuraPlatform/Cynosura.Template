﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Data;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.Files.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cynosura.Template.Core.Requests.Files
{
    public class GetFileHandler : IRequestHandler<GetFile, FileModel>
    {
        private readonly IEntityRepository<File> _fileRepository;
        private readonly IMapper _mapper;

        public GetFileHandler(IEntityRepository<File> fileRepository,
            IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<FileModel> Handle(GetFile request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetEntities()
                .Include(e => e.Group)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            return _mapper.Map<File, FileModel>(file);
        }

    }
}
