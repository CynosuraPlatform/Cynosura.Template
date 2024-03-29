﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using Cynosura.Template.Core.Entities;

namespace Cynosura.Template.Core.Requests.WorkerInfos
{
    public class DeleteWorkerInfoHandler : IRequestHandler<DeleteWorkerInfo>
    {
        private readonly IEntityRepository<WorkerInfo> _workerInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteWorkerInfoHandler(IEntityRepository<WorkerInfo> workerInfoRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _workerInfoRepository = workerInfoRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task Handle(DeleteWorkerInfo request, CancellationToken cancellationToken)
        {
            var workerInfo = await _workerInfoRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (workerInfo == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Worker"], request.Id]);
            }
            _workerInfoRepository.Delete(workerInfo);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

    }
}
