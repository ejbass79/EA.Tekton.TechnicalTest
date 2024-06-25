using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Cross.Auth.Errors;
using EA.Tekton.TechnicalTest.Cross.Auth.Interfaces;
using EA.Tekton.TechnicalTest.Cross.Auth.Models;

namespace EA.Tekton.TechnicalTest.Cross.Auth.CQRS.Components.AuthComponent.Commands;

public class RegisterUserCommandHandler(IAuthService authService) : ICommandHandler<RegisterUserCommand>
{
    public async Task<Result> Handle(RegisterUserCommand registerUser, CancellationToken cancellationToken)
    {
        var user = new AuthUser
        {
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            Email = registerUser.Email.ToUpper(),
            Token = registerUser.Token
        };

        var result = await authService.RegisterUserAsync(user, registerUser.Password).ConfigureAwait(false);

        return !result.Succeeded ? Result.Failure(AuthErrors.FailedRegister) : Result.Success();
    }
}
