using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Cynosura.Core.Data;
using Cynosura.Core.Messaging;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Messaging.WorkerRuns;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cynosura.Template.Core.Messaging.WorkerInfos
{
    public class RunWorkerInfoHandler : IRequestHandler<RunWorkerInfo>
    {
        private readonly IEntityRepository<WorkerRun> _workerRunRepository;
        private readonly IEntityRepository<WorkerInfo> _workerInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagingService _messagingService;

        public RunWorkerInfoHandler(IEntityRepository<WorkerRun> workerRunRepository,
            IEntityRepository<WorkerInfo> workerInfoRepository,
            IUnitOfWork unitOfWork,
            IMessagingService messagingService)
        {
            _workerRunRepository = workerRunRepository;
            _workerInfoRepository = workerInfoRepository;
            _unitOfWork = unitOfWork;
            _messagingService = messagingService;
        }

        public async Task Handle(RunWorkerInfo request, CancellationToken cancellationToken)
        {
            var workerInfo = await _workerInfoRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstAsync();
            var workerRun = new WorkerRun
            {
                WorkerInfoId = request.Id,
                Data = request.Data != null ? JsonSerializer.Serialize(request.Data) : null,
            };
            if (workerInfo.RetryCount != null)
            {
                workerRun.TriesLeft = request.TriesLeft ?? workerInfo.RetryCount - 1;
            }
            _workerRunRepository.Add(workerRun);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _messagingService.SendAsync(StartWorkerRun.QueueName, new StartWorkerRun(workerRun.Id));
        }
    }
}
