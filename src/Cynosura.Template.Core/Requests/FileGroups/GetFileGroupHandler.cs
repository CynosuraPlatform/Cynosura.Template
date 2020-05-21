﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Data;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.FileGroups.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cynosura.Template.Core.Requests.FileGroups
{
    public class GetFileGroupHandler : IRequestHandler<GetFileGroup, FileGroupModel>
    {
        private readonly IEntityRepository<FileGroup> _fileGroupRepository;
        private readonly IMapper _mapper;

        public GetFileGroupHandler(IEntityRepository<FileGroup> fileGroupRepository,
            IMapper mapper)
        {
            _fileGroupRepository = fileGroupRepository;
            _mapper = mapper;
        }

        public async Task<FileGroupModel> Handle(GetFileGroup request, CancellationToken cancellationToken)
        {
            var fileGroup = await _fileGroupRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            return _mapper.Map<FileGroup, FileGroupModel>(fileGroup);
        }

    }
}
