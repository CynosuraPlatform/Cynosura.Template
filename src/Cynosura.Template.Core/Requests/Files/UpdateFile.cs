using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Cynosura.Template.Core.Requests.Files
{
    public class UpdateFile : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public string Url { get; set; }

        public int? GroupId { get; set; }
    }
}
