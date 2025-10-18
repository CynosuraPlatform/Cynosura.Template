using System;
using System.Collections.Generic;

namespace Cynosura.Template.Core.Requests.WorkerScheduleTasks.Models
{
    public class WorkerScheduleTaskShortModel
    {

        public int Id { get; set; }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}
