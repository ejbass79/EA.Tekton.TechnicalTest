using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Queries;

public class GetAllStateForeignKeyByStatusIdQueryHandler(IProductService service) 
    : ICommandHandler<GetAllStateForeignKeyByStatusIdQuery, IEnumerable<ProductDto>>
{
    private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<IEnumerable<ProductDto>>> Handle(GetAllStateForeignKeyByStatusIdQuery request, CancellationToken cancellationToken)
    {
        return await GetAllStateForeignKeyByStatusIdAsync(request.StatusId).ConfigureAwait(false);
    }

    private async Task<Result<IEnumerable<ProductDto>>> GetAllStateForeignKeyByStatusIdAsync(int statusId)
    {
        var result = await _service.GetAllByStatusIdAsync(statusId).ConfigureAwait(false);
        
        return Result.Success(result);
    }
}