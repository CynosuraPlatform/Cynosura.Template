using System;
using Cynosura.Template.Core.Infrastructure;

namespace Cynosura.Template.Core.Requests.WorkerInfos.Models
{
    public class WorkerInfoFilter : EntityFilter
    {
        public string? Name { get; set; }
        public string? ClassName { get; set; }
        public int? RetryCountFrom { get; set; }
        public int? RetryCountTo { get; set; }
        public TimeSpan? RetryIntervalFrom { get; set; }
        public TimeSpan? RetryIntervalTo { get; set; }
    }
}
