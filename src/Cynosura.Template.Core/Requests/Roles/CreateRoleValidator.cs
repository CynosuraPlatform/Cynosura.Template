using FluentValidation;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class CreateRoleValidator : AbstractValidator<CreateRole>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithName("Name");
        }
    }
}
