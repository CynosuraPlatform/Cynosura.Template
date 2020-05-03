using FluentValidation;

namespace Cynosura.Template.Core.Requests.Users
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email).MaximumLength(256).EmailAddress().NotEmpty();
            RuleFor(x => x.Password).Length(6, 100).NotEmpty();
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match");
            RuleFor(x => x.FirstName).MaximumLength(200);
            RuleFor(x => x.LastName).MaximumLength(200);
        }

    }
}
