﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Definition;
using Cynosura.Template.Core.Messaging.WorkerRuns;

namespace Cynosura.Template.Worker.Consumers
{
    public class StartWorkerRunConsumerDefinition : ConsumerDefinition<StartWorkerRunConsumer>
    {
        public StartWorkerRunConsumerDefinition()
        {
            EndpointName = StartWorkerRun.QueueName;
        }
    }
}
