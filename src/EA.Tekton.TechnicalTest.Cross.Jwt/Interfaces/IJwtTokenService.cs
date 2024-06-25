using System.Security.Claims;
using EA.Tekton.TechnicalTest.Cross.Jwt.Models;

namespace EA.Tekton.TechnicalTest.Cross.Jwt.Interfaces;

public interface IJwtTokenService
{
    Task<JwtTokenResult> GenerateToken(string user, string email, IList<string>? roles, CancellationToken cancellationToken);

    //Task<JwtTokenResult> GenerateClaimsTokenAsync(string username, CancellationToken cancellationToken);

    //Task<ClaimsPrincipal?> GetPrincipalFromTokenAsync(string token);

    //Task<JwtTokenResult> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
}
