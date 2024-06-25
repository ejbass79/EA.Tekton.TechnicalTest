using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Commands;

internal class UpdateStateCommandHandler(IStateService service) : ICommandHandler<UpdateStateCommand, bool>
{
    private readonly IStateService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<bool>> Handle(UpdateStateCommand request, CancellationToken cancellationToken)
    {
        return await UpdateAsync(request.StatusId, request.State).ConfigureAwait(false);
    }

    private async Task<Result<bool>> UpdateAsync(int statusId, StateDto entity)
    {
        var result = await _service.PutAsync(statusId, entity).ConfigureAwait(false);

        return Result.Success(result);
    }
}