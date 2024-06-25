using EA.Tekton.TechnicalTest.Domain.Dto;
using FluentValidation;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Commands;

public class UpdateStateCommandValidator : AbstractValidator<StateDto>
{
    public UpdateStateCommandValidator()
    {
        //RuleFor(x => x.Id)
        //    .NotEmpty()
        //    .NotNull()
        //    .MaximumLength(2);

        //RuleFor(x => x.Description)
        //    .NotEmpty()
        //    .NotNull()
        //    .MaximumLength(200);
    }
}