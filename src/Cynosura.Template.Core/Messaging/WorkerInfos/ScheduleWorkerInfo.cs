using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cynosura.Template.Core.Messaging.WorkerInfos
{
    public class ScheduleWorkerInfo
    {
        public static string QueueName => typeof(ScheduleWorkerInfo).FullName!;

        public int Id { get; set; }
    }
}
