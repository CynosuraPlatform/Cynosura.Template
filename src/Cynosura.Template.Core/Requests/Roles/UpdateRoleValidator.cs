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
