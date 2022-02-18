using System;
using MediatR;
using Cynosura.Template.Core.Requests.FileGroups.Models;

namespace Cynosura.Template.Core.Requests.FileGroups
{
    public class GetFileGroup : IRequest<FileGroupModel?>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}
