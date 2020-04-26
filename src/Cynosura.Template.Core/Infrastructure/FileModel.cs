using System;
using System.Collections.Generic;
using System.Text;

namespace Cynosura.Template.Core.Infrastructure
{
    public class FileModel
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
