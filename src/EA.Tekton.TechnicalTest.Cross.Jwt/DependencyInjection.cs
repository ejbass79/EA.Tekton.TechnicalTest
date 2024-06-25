using EA.Tekton.TechnicalTest.Cross.Jwt.Interfaces;
using EA.Tekton.TechnicalTest.Cross.Jwt.Options;
using EA.Tekton.TechnicalTest.Cross.Jwt.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace EA.Tekton.TechnicalTest.Cross.Jwt;

public static class DependencyInjection
{
    public static IServiceCollection AddCrossJwtService(this IServiceCollection services, JwtOptions jwtOptions)
    {
        // Configure JWT Authentication and Authorization
        services.AddTransient<IJwtTokenService, JwtTokenService>();

        services.AddSingleton(jwtOptions);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = jwtOptions.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
            ValidIssuer = jwtOptions.Issuer,
            ValidateIssuer = jwtOptions.ValidateIssuer,
            ValidateAudience = jwtOptions.ValidateAudience,
            ValidAudience = jwtOptions.Audience,
            RequireExpirationTime = jwtOptions.RequireExpirationTime,
            ValidateLifetime = jwtOptions.ValidateLifetime,
            ClockSkew = jwtOptions.Expiration
        };

        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.SaveToken = false;
            });

        return services;
    }
}
