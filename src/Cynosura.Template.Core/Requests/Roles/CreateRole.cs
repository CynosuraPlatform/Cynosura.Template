using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cynosura.Template.Core.Infrastructure;
using MediatR;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class CreateRole : IRequest<CreatedEntity<int>>
    {
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
    }
}
