﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cynosura.Template.Core.Messaging.WorkerRuns;
using MassTransit;
using MediatR;

namespace Cynosura.Template.Worker.Consumer
{
    public class StartWorkerRunConsumer : IConsumer<StartWorkerRun>
    {
        private readonly IMediator _mediator;

        public StartWorkerRunConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<StartWorkerRun> context)
        {
            await _mediator.Send(context.Message, context.CancellationToken);
        }
    }
}
