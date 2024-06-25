using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Cross.Auth.Interfaces;

namespace EA.Tekton.TechnicalTest.Cross.Auth.CQRS.Components.AuthComponent.Queries;

public class GetUserRolesQueryHandler(IAuthService authService) : ICommandHandler<GetUserRolesQuery, List<string>>
{
    public async Task<Result<List<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await authService.GetUserRolesAsync(request.Email).ConfigureAwait(false);

        var result = roles == null ? Enumerable.Empty<string>().ToList() : roles.ToList();

        return Result.Success(result);
    }
}
