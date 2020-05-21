﻿using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Cynosura.Template.Core.Requests.Files
{
    public class CreateFileValidator : AbstractValidator<CreateFile>
    {
        public CreateFileValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty().WithName(x => localizer["Name"]);
            RuleFor(x => x.ContentType).MaximumLength(200).NotEmpty().WithName(x => localizer["Content Type"]);
            RuleFor(x => x.Content);
            RuleFor(x => x.Url).MaximumLength(200).WithName(x => localizer["Url"]);
            RuleFor(x => x.GroupId).NotEmpty().WithName(x => localizer["Group"]);
        }

    }
}
