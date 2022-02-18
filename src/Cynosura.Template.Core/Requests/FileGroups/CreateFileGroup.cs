using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Cynosura.Template.Core.Infrastructure;

namespace Cynosura.Template.Core.Requests.FileGroups
{
    public class CreateFileGroup : IRequest<CreatedEntity<int>>
    {
        public string? Name { get; set; }

        public Enums.FileGroupType? Type { get; set; }

        public string? Location { get; set; }

        public string? Accept { get; set; }
    }
}
