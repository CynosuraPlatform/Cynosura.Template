using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Cynosura.Template.Core.Requests.Files
{
    public class CreateFileHandler : IRequestHandler<CreateFile, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Entities.File> _fileRepository;
        private readonly IEntityRepository<FileGroup> _fileGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CreateFileHandler(IEntityRepository<Entities.File> fileRepository,
            IEntityRepository<FileGroup> fileGroupRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _fileRepository = fileRepository;
            _fileGroupRepository = fileGroupRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<CreatedEntity<int>> Handle(CreateFile request, CancellationToken cancellationToken)
        {
            var fileGroup = await _fileGroupRepository.GetEntities()
                .Where(e => e.Id == request.GroupId)
                .FirstOrDefaultAsync();
            if (fileGroup == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["File Group"], request.GroupId]);
            }
            fileGroup.Accept.Validate(request.Name, request.ContentType);
            var file = _mapper.Map<CreateFile, Entities.File>(request);
            if (fileGroup.Type == Enums.FileGroupType.Database)
            {
                file.Content = request.Content.ConvertToBytes();
            }
            _fileRepository.Add(file);
            await _unitOfWork.CommitAsync();
            return new CreatedEntity<int>() { Id = file.Id };
        }
    }
}
