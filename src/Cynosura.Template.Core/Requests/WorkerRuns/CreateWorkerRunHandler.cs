﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using Cynosura.Core.Messaging;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Messaging.WorkerRuns;

namespace Cynosura.Template.Core.Requests.WorkerRuns
{
    public class CreateWorkerRunHandler : IRequestHandler<CreateWorkerRun, CreatedEntity<int>>
    {
        private readonly IEntityRepository<WorkerRun> _workerRunRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagingService _messagingService;
        private readonly IMapper _mapper;

        public CreateWorkerRunHandler(IEntityRepository<WorkerRun> workerRunRepository,
            IUnitOfWork unitOfWork,
            IMessagingService messagingService,
            IMapper mapper)
        {
            _workerRunRepository = workerRunRepository;
            _unitOfWork = unitOfWork;
            _messagingService = messagingService;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateWorkerRun request, CancellationToken cancellationToken)
        {
            var workerRun = _mapper.Map<CreateWorkerRun, WorkerRun>(request);
            _workerRunRepository.Add(workerRun);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _messagingService.SendAsync(StartWorkerRun.QueueName, new StartWorkerRun(workerRun.Id));
            return new CreatedEntity<int>(workerRun.Id);
        }

    }
}
