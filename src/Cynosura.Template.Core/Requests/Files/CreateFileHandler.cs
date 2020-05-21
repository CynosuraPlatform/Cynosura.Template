using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Data;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Infrastructure;
using MediatR;

namespace Cynosura.Template.Core.Requests.Files
{
    public class CreateFileHandler : IRequestHandler<CreateFile, CreatedEntity<int>>
    {
        private readonly IEntityRepository<File> _fileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFileHandler(IEntityRepository<File> fileRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateFile request, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<CreateFile, File>(request);
            _fileRepository.Add(file);
            await _unitOfWork.CommitAsync();
            return new CreatedEntity<int>() { Id = file.Id };
        }

    }
}
