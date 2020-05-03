using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Cynosura.Template.Core.Requests.Profile
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfile>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.FirstName).MaximumLength(200);
            RuleFor(x => x.LastName).MaximumLength(200);
        }
    }
}
