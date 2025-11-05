using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cynosura.Template.Core.Messaging.WorkerRuns;
using Cynosura.Template.Worker.WorkerInfos;
using MassTransit;

namespace Cynosura.Template.Worker.Consumers
{
    public class RetryWorkerRunConsumer : IConsumer<RetryWorkerRun>
    {
        private readonly WorkerInfoSheduler _workerInfoSheduler;

        public RetryWorkerRunConsumer(WorkerInfoSheduler workerInfoSheduler)
        {
            _workerInfoSheduler = workerInfoSheduler;
        }

        public async Task Consume(ConsumeContext<RetryWorkerRun> context)
        {
            await _workerInfoSheduler.RetryAsync(context.Message.Id);
        }
    }
}
