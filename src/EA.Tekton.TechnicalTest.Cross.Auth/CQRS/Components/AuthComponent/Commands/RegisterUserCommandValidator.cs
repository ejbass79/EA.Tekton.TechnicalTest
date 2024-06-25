using FluentValidation;

namespace EA.Tekton.TechnicalTest.Cross.Auth.CQRS.Components.AuthComponent.Commands;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(v => v.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(v => v.LastName)
            .MaximumLength(100);

        RuleFor(v => v.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();

        RuleFor(v => v.Password)
            .NotEmpty();
    }
}
