using EA.Tekton.TechnicalTest.Cross.Abstractions.Behaviors;
using EA.Tekton.TechnicalTest.Cross.Auth.Interfaces;
using EA.Tekton.TechnicalTest.Cross.Auth.Services;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace EA.Tekton.TechnicalTest.Cross.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddCrossAuthService(this IServiceCollection services)
    {
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
