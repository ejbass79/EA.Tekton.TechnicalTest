using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Queries;

public class GetProductByProductIdQueryHandler(IProductService service) : ICommandHandler<GetProductByProductIdQuery, ProductResponse>
{
    private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<ProductResponse>> Handle(GetProductByProductIdQuery request, CancellationToken cancellationToken)
    {
        return await GetProductByProductIdAsync(request.ProductId).ConfigureAwait(false);
    }

    private async Task<Result<ProductResponse>> GetProductByProductIdAsync(int productId)
    {
        var result = await _service.GetByProductIdAsync(productId).ConfigureAwait(false);
        
        return Result.Success(result);
    }
}