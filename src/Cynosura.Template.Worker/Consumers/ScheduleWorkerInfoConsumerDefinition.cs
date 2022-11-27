using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit.Definition;
using Cynosura.Template.Core.Messaging.WorkerInfos;

namespace Cynosura.Template.Worker.Consumers
{
    public class ScheduleWorkerInfoConsumerDefinition : ConsumerDefinition<ScheduleWorkerInfoConsumer>
    {
        public ScheduleWorkerInfoConsumerDefinition()
        {
            EndpointName = ScheduleWorkerInfo.QueueName;
        }
    }
}
