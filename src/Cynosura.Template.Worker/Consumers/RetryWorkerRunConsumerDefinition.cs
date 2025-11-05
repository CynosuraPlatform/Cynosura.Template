using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cynosura.Template.Core.Messaging.WorkerRuns;
using MassTransit.Definition;

namespace Cynosura.Template.Worker.Consumers
{
    public class RetryWorkerRunConsumerDefinition : ConsumerDefinition<RetryWorkerRunConsumer>
    {
        public RetryWorkerRunConsumerDefinition()
        {
            EndpointName = RetryWorkerRun.QueueName;
            ConcurrentMessageLimit = 1;
        }
    }
}
