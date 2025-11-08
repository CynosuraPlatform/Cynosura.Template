using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynosura.Template.Core.Messaging.WorkerRuns
{
    public class RetryWorkerRun
    {
        public static string QueueName => typeof(RetryWorkerRun).FullName!;

        public int Id { get; set; }
    }
}
