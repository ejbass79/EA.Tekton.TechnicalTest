using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Commands;

internal class UpdateProductCommandHandler(IProductService service) : ICommandHandler<UpdateProductCommand, bool>
{
    private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        return await UpdateAsync(request.ProductId, request.Product).ConfigureAwait(false);
    }

    private async Task<Result<bool>> UpdateAsync(int productId, ProductDto entity)
    {
        var result = await _service.PutAsync(productId, entity).ConfigureAwait(false);

        return Result.Success(result);
    }
}