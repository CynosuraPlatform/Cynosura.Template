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
using Cynosura.Template.Core.Messaging.WorkerInfos;

namespace Cynosura.Template.Core.Requests.WorkerScheduleTasks
{
    public class CreateWorkerScheduleTaskHandler : IRequestHandler<CreateWorkerScheduleTask, CreatedEntity<int>>
    {
        private readonly IEntityRepository<WorkerScheduleTask> _workerScheduleTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagingService _messagingService;
        private readonly IMapper _mapper;

        public CreateWorkerScheduleTaskHandler(IEntityRepository<WorkerScheduleTask> workerScheduleTaskRepository,
            IUnitOfWork unitOfWork,
            IMessagingService messagingService,
            IMapper mapper)
        {
            _workerScheduleTaskRepository = workerScheduleTaskRepository;
            _unitOfWork = unitOfWork;
            _messagingService = messagingService;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateWorkerScheduleTask request, CancellationToken cancellationToken)
        {
            var workerScheduleTask = _mapper.Map<CreateWorkerScheduleTask, WorkerScheduleTask>(request);
            _workerScheduleTaskRepository.Add(workerScheduleTask);
            await _unitOfWork.CommitAsync();
            await _messagingService.SendAsync(ScheduleWorkerInfo.QueueName, new ScheduleWorkerInfo
            {
                Id = workerScheduleTask.WorkerInfoId
            });
            return new CreatedEntity<int>(workerScheduleTask.Id);
        }

    }
}
