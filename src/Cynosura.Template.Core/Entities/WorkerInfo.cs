using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cynosura.Template.Core.Entities
{
    public class WorkerInfo : BaseEntity
    {
        [Required()]
        [StringLength(200)]
        public required string Name { get; set; }
        
        [Required()]
        [StringLength(200)]
        public required string ClassName { get; set; }

        public IList<WorkerScheduleTask> ScheduleTasks { get; set; } = null!;

        public int? RetryCount { get; set; }
                

        public TimeSpan? RetryInterval { get; set; }

    }
}
