using EA.Tekton.TechnicalTest.Cross.Jwt.Interfaces;
using EA.Tekton.TechnicalTest.Cross.Jwt.Models;
using EA.Tekton.TechnicalTest.Cross.Jwt.Options;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EA.Tekton.TechnicalTest.Cross.Jwt.Services;

public class JwtTokenService(JwtOptions jwtOptions) : IJwtTokenService
{
    private readonly JwtOptions _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));

    public async Task<JwtTokenResult> GenerateToken(string user, string email, IList<string>? roles, CancellationToken cancellationToken)
    {
        var listClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.UniqueName, user),
                new (JwtRegisteredClaimNames.Email, email),
                new (JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                new (JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddMinutes(_jwtOptions.Expiration.TotalMinutes)).ToUnixTimeSeconds().ToString()),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        if (roles != null && roles.Any())
            roles.ToList().ForEach(rol =>
                {
                    listClaims.Add(new Claim(ClaimTypes.Role, rol));
                }
            );

        var expiration = DateTime.Now.AddMinutes(_jwtOptions.Expiration.TotalMinutes);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            listClaims,
            expires: expiration,
            signingCredentials: cred);

        return new JwtTokenResult
        {
            Succeeded = true,
            AccessToken = await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token), cancellationToken),
            ExpiresIn = (int)_jwtOptions.Expiration.TotalMinutes,
            //RefreshToken = GenerateRefreshToken() // refreshToken.Token
        };
    }
}
