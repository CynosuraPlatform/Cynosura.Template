using FluentValidation;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRole>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Name).MaximumLength(256).NotEmpty();
            RuleFor(x => x.DisplayName).MaximumLength(100).NotEmpty();
        }

    }
}
