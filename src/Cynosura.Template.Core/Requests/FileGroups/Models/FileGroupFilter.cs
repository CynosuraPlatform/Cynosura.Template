using System;
using Cynosura.Template.Core.Infrastructure;

namespace Cynosura.Template.Core.Requests.FileGroups.Models
{
    public class FileGroupFilter : EntityFilter
    {
        public string Name { get; set; }
        public Enums.FileGroupType? Type { get; set; }
        public string Location { get; set; }
        public string Accept { get; set; }
    }
}
