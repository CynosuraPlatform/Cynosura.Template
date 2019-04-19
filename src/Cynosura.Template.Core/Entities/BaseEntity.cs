﻿using System;

namespace Cynosura.Template.Core.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int? CreationUserId { get; set; }
        public User CreationUser { get; set; }
        public int? ModificationUserId { get; set; }
        public User ModificationUser { get; set; }
    }
}
