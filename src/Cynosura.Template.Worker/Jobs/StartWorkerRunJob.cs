﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Quartz;
using Cynosura.Template.Core.Messaging.WorkerRuns;
using Cynosura.Template.Worker.Infrastructure;

namespace Cynosura.Template.Worker.Jobs
{
    [DisallowConcurrentExecution]
    public class StartWorkerRunJob : IJob
    {
        public static string JobKey => nameof(StartWorkerRunJob);

        private readonly IMediator _mediator;

        public StartWorkerRunJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var message = (StartWorkerRun)context.Trigger.JobDataMap[QuartzData.Message];
            await _mediator.Send(message);
        }
    }
}
