using EA.Tekton.TechnicalTest.Cross.Abstractions;
using EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;
using EA.Tekton.TechnicalTest.Cross.Auth.Interfaces;
using EA.Tekton.TechnicalTest.Cross.Auth.Models;
using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Cross.Exceptions;
using EA.Tekton.TechnicalTest.Cross.Jwt.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EA.Tekton.TechnicalTest.Cross.Auth.CQRS.Components.AuthComponent.Commands;

public class LoginCommandHandler(IAuthService authService, IJwtTokenService jwtTokenService) : ICommandHandler<LoginCommand, AuthToken>
{
    private readonly IAuthService _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));

    public async Task<Result<AuthToken>> Handle(LoginCommand loginCommand, CancellationToken cancellationToken)
    {
        return await GetLoginAsync(loginCommand, cancellationToken).ConfigureAwait(false);
    }

    private async Task<Result<AuthToken>> GetLoginAsync(LoginCommand loginCommand, CancellationToken cancellationToken)
    {
        var result = await _authService.PasswordSignInAsync(loginCommand.Email.ToUpper(), loginCommand.Password, lockoutOnFailure: false).ConfigureAwait(false);

        if (!result.Succeeded)
        {
            throw new UnauthorizedException("Invalid email and password combination.");
        }

        var user = await _authService.GetUserAsync(loginCommand.Email).ConfigureAwait(false);

        var roles = await _authService.GetUserRolesAsync(loginCommand.Email).ConfigureAwait(false);

        var response = await _jwtTokenService.GenerateToken(loginCommand.Email, loginCommand.Email, roles, cancellationToken).ConfigureAwait(false);

        var authToken = new AuthToken
        {
            AccessToken = response.AccessToken,
            TokenType = JwtBearerDefaults.AuthenticationScheme,
            ExpiresIn = response.ExpiresIn,
            RefreshToken = response.RefreshToken,
            User = new TokenInfo
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            },
            Roles = roles!.ToList()
        };

        return Result.Success(authToken);
    }
}
