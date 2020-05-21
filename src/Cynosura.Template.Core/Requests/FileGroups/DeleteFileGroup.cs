using System;
using MediatR;

namespace Cynosura.Template.Core.Requests.FileGroups
{
    public class DeleteFileGroup : IRequest
    {
        public int Id { get; set; }
    }
}
