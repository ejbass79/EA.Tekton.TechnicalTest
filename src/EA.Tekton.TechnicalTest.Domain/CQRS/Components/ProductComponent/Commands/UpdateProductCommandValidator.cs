using EA.Tekton.TechnicalTest.Domain.Dto;
using FluentValidation;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Commands;

public class UpdateProductCommandValidator : AbstractValidator<ProductDto>
{
    public UpdateProductCommandValidator()
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