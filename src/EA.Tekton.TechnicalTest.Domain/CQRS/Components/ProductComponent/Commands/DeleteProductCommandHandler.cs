using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

using System;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Commands;

internal class DeleteProductCommandHandler(IProductService service) : ICommandHandler<DeleteProductCommand, bool>
{
    private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return await DeleteAsync(request.ProductId).ConfigureAwait(false);
    }

    private async Task<Result<bool>> DeleteAsync(int productId)
    {
        var result = await _service.DeleteAsync(productId).ConfigureAwait(false);

        return Result.Success(result);
    }
}