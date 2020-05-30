using System;
using MediatR;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Files.Models;

namespace Cynosura.Template.Core.Requests.Files
{
    public class DownloadFile : IRequest<FileContentModel>
    {
        public int Id { get; set; }
    }
}
