using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

using System;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Commands;

internal class DeleteStateCommandHandler(IStateService service) : ICommandHandler<DeleteStateCommand, bool>
{
    private readonly IStateService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<bool>> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
    {
        return await DeleteAsync(request.StatusId).ConfigureAwait(false);
    }

    private async Task<Result<bool>> DeleteAsync(int statusId)
    {
        var result = await _service.DeleteAsync(statusId).ConfigureAwait(false);

        return Result.Success(result);
    }
}