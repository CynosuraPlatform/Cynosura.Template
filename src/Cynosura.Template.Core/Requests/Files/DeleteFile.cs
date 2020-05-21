using System;
using MediatR;

namespace Cynosura.Template.Core.Requests.Files
{
    public class DeleteFile : IRequest
    {
        public int Id { get; set; }
    }
}
