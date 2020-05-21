using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using Cynosura.Template.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Cynosura.Template.Core.Requests.Files
{
    public class DeleteFileHandler : IRequestHandler<DeleteFile>
    {
        private readonly IEntityRepository<File> _fileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteFileHandler(IEntityRepository<File> fileRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteFile request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            if (file == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["File"], request.Id]);
            }
            _fileRepository.Delete(file);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

    }
}
