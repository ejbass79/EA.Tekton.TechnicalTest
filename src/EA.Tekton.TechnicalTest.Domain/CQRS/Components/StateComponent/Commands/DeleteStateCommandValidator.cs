using EA.Tekton.TechnicalTest.Domain.Dto;
using FluentValidation;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Commands;

public class DeleteStateCommandValidator : AbstractValidator<StateDto>
{
    public DeleteStateCommandValidator()
    {
        //RuleFor(x => x.Id)
        //    .NotEmpty()
        //    .NotNull()
        //    .MaximumLength(2);
    }
}