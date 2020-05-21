using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using Cynosura.Template.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Cynosura.Template.Core.Requests.Files
{
    public class UpdateFileHandler : IRequestHandler<UpdateFile>
    {
        private readonly IEntityRepository<File> _fileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateFileHandler(IEntityRepository<File> fileRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateFile request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetEntities()
                .Include(e => e.Group)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            if (file == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["File"], request.Id]);
            }
            file.Group.Accept.Validate(request.Name, request.ContentType);
            _mapper.Map(request, file);
            if (file.Group.Type == Enums.FileGroupType.Database)
            {
                file.Content = request.Content.ConvertToBytes();
            }
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

    }
}
