using EA.Tekton.TechnicalTest.Domain.Dto;
using FluentValidation;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Commands;

public class DeleteProductCommandValidator : AbstractValidator<ProductDto>
{
    public DeleteProductCommandValidator()
    {
        //RuleFor(x => x.Id)
        //    .NotEmpty()
        //    .NotNull()
        //    .MaximumLength(2);
    }
}