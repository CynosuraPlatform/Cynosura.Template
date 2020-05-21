using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cynosura.Template.Core.Infrastructure;
using MediatR;

namespace Cynosura.Template.Core.Requests.Files
{
    public class CreateFile : IRequest<CreatedEntity<int>>
    {
        public string Name { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public string Url { get; set; }

        public int? GroupId { get; set; }
    }
}
