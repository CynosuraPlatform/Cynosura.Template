﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRole>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
