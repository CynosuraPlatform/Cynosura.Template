﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class UpdateRole : IRequest
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
    }
}
