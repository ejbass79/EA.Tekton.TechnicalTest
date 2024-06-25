using FluentValidation;

namespace EA.Tekton.TechnicalTest.Cross.Auth.CQRS.Components.AuthComponent.Queries
{
    public class GetUserRolesQueryValidator : AbstractValidator<GetUserRolesQuery>
    {
        public GetUserRolesQueryValidator()
        {
            RuleFor(v => v.Email).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Token field is required.")
                .EmailAddress().WithMessage("Invalid email address format.");
        }
    }
}
