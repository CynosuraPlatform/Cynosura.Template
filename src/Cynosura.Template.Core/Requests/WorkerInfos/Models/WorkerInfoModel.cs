﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cynosura.Template.Core.Requests.WorkerInfos.Models
{
    public class WorkerInfoModel
    {
        public WorkerInfoModel(string name, string className)
        {
            Name = name;
            ClassName = className;
        }

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modification Date")]
        public DateTime ModificationDate { get; set; }

        [DisplayName("Creation User")]
        public int? CreationUserId { get; set; }
        public Users.Models.UserShortModel? CreationUser { get; set; }

        [DisplayName("Modification User")]
        public int? ModificationUserId { get; set; }
        public Users.Models.UserShortModel? ModificationUser { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Class Name")]
        public string ClassName { get; set; }
    }
}
