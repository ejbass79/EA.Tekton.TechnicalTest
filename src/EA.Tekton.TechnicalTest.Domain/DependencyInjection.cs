using EA.Tekton.TechnicalTest.Cross.Abstractions.Behaviors;
using EA.Tekton.TechnicalTest.Cross.Auth.Interfaces;
using EA.Tekton.TechnicalTest.Cross.Auth.Services;
using EA.Tekton.TechnicalTest.Domain.Interfaces;
using EA.Tekton.TechnicalTest.Domain.Services;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace EA.Tekton.TechnicalTest.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainService(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Register Domain Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IStateService, StateService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}