using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Commands;

internal class CreateProductCommandHandler(IProductService service) : ICommandHandler<CreateProductCommand, (bool status, int productId)>
{
    private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<(bool status, int productId)>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        return await CreateAsync(request.Product).ConfigureAwait(false);
    }

    private async Task<Result<(bool status, int productId)>> CreateAsync(ProductDto entity)
    {
        var result = await _service.PostAsync(entity).ConfigureAwait(false);

        return Result.Create(result);
    }
}