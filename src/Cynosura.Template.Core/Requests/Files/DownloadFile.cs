using System;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Files.Models;
using MediatR;

namespace Cynosura.Template.Core.Requests.Files
{
    public class DownloadFile : IRequest<FileContentModel>
    {
        public int Id { get; set; }
    }
}
