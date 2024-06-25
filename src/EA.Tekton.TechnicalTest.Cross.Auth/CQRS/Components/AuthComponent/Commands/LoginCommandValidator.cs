using FluentValidation;

namespace EA.Tekton.TechnicalTest.Cross.Auth.CQRS.Components.AuthComponent.Commands;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MaximumLength(400);
    }
}
