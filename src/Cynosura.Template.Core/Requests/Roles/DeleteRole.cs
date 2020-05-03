using System;
using MediatR;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class DeleteRole : IRequest
    {
        public int Id { get; set; }
    }
}
