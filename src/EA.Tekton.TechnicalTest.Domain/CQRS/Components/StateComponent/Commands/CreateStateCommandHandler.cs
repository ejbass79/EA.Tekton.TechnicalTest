using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

namespace EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Commands;

internal class CreateStateCommandHandler(IStateService service) : ICommandHandler<CreateStateCommand, (bool status, int statusId)>
{
    private readonly IStateService _service = service ?? throw new ArgumentNullException(nameof(service));

    public async Task<Result<(bool status, int statusId)>> Handle(CreateStateCommand request, CancellationToken cancellationToken)
    {
        return await CreateAsync(request.State).ConfigureAwait(false);
    }

    private async Task<Result<(bool status, int statusId)>> CreateAsync(StateDto entity)
    {
        var result = await _service.PostAsync(entity).ConfigureAwait(false);

        return Result.Create(result);
    }
}