using System;
using MediatR;
using Cynosura.Template.Core.Requests.Files.Models;

namespace Cynosura.Template.Core.Requests.Files
{
    public class GetFile : IRequest<FileModel?>
    {
        public int Id { get; set; }
    }
}
