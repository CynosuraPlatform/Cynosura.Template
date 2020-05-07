using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class UpdateRole : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
