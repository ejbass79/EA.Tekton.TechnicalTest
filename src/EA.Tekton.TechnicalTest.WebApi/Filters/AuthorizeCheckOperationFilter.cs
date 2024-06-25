using EA.Tekton.TechnicalTest.Cross.Configuration;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

using System.Net;

namespace EA.Tekton.TechnicalTest.WebApi.Filters
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType != null &&
                               (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>()
                                   .Any() || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>()
                                   .Any());

            if (!hasAuthorize) return;

            operation.Responses.TryAdd($"{(int)HttpStatusCode.BadRequest}", new OpenApiResponse { Description = "Bad Request" });
            operation.Responses.TryAdd($"{(int)HttpStatusCode.Unauthorized}", new OpenApiResponse { Description = "Unauthorized - Not Authenticated" });
            operation.Responses.TryAdd($"{(int)HttpStatusCode.Forbidden}", new OpenApiResponse { Description = "Forbidden - Not Authorized" });
            operation.Responses.TryAdd($"{(int)HttpStatusCode.InternalServerError}", new OpenApiResponse { Description = "Internal Server Error" });

            var bearerScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme },
                Description = CrossConfiguration.JwtAuthorizationDescription,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [bearerScheme] = new [] {string.Empty}
                }
            };
        }
    }
}
