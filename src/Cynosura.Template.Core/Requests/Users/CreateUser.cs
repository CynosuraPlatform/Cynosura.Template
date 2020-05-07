﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cynosura.Template.Core.Infrastructure;
using MediatR;

namespace Cynosura.Template.Core.Requests.Users
{
    public class CreateUser : IRequest<CreatedEntity<int>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public List<int> RoleIds { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
