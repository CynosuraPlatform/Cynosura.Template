﻿using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRole>
    {
        public UpdateRoleValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(256).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.DisplayName).MaximumLength(100).NotEmpty().WithName(x => localizer["Display Name"]);
        }

    }
}
