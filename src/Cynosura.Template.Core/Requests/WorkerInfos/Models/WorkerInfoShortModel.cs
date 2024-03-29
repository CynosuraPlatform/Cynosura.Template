﻿using System;
using System.Collections.Generic;

namespace Cynosura.Template.Core.Requests.WorkerInfos.Models
{
    public class WorkerInfoShortModel
    {
        public WorkerInfoShortModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
