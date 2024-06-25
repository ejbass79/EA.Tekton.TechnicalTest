using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Queries;

public class GetStateAllQueryHandler(IStateService service) : ICommandHandler<GetStateAllQuery, QueryMultipleResponse<StateDto>>
{
    private readonly IStateService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<QueryMultipleResponse<StateDto>>> Handle(GetStateAllQuery request, CancellationToken cancellationToken)
    {
        return await GetStateAllAsync(request.Page, request.Limit, request.OrderBy, request.Ascending).ConfigureAwait(false);
    }

    private async Task<Result<QueryMultipleResponse<StateDto>>> GetStateAllAsync(int page, int limit, string orderBy, bool ascending = true)
    {
        var result = await _service.GetAllAsync(page, limit, orderBy, ascending).ConfigureAwait(false);

        return Result.Success(result);
    }
}