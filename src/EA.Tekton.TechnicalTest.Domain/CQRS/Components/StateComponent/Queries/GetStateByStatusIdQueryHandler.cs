using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Queries;

public class GetStateByStatusIdQueryHandler(IStateService service) : ICommandHandler<GetStateByStatusIdQuery, StateDto>
{
    private readonly IStateService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<StateDto>> Handle(GetStateByStatusIdQuery request, CancellationToken cancellationToken)
    {
        return await GetStateByStatusIdAsync(request.StatusId).ConfigureAwait(false);
    }

    private async Task<Result<StateDto>> GetStateByStatusIdAsync(int statusId)
    {
        var result = await _service.GetByStatusIdAsync(statusId).ConfigureAwait(false);
        
        return Result.Success(result);
    }
}