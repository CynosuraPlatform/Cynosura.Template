using System;
using Cynosura.Template.Core.Requests.Files.Models;
using MediatR;

namespace Cynosura.Template.Core.Requests.Files
{
    public class GetFile : IRequest<FileModel>
    {
        public int Id { get; set; }
    }
}
