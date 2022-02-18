using System;
using Cynosura.Template.Core.Infrastructure;

namespace Cynosura.Template.Core.Requests.WorkerInfos.Models
{
    public class WorkerInfoFilter : EntityFilter
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
    }
}
