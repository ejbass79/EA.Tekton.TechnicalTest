using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Queries;

public class GetProductAllQueryHandler(IProductService service) : ICommandHandler<GetProductAllQuery, QueryMultipleResponse<ProductDto>>
{
    private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<QueryMultipleResponse<ProductDto>>> Handle(GetProductAllQuery request, CancellationToken cancellationToken)
    {
        return await GetProductAllAsync(request.Page, request.Limit, request.OrderBy, request.Ascending).ConfigureAwait(false);
    }

    private async Task<Result<QueryMultipleResponse<ProductDto>>> GetProductAllAsync(int page, int limit, string orderBy, bool ascending = true)
    {
        var result = await _service.GetAllAsync(page, limit, orderBy, ascending).ConfigureAwait(false);

        return Result.Success(result);
    }
}