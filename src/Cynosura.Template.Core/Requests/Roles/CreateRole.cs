using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Cynosura.Template.Core.Infrastructure;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class CreateRole : IRequest<CreatedEntity<int>>
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }
    }
}
