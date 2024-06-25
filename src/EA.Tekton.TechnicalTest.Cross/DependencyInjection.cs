using EA.Tekton.TechnicalTest.Cross.CurrentUser;

using Microsoft.Extensions.DependencyInjection;

namespace EA.Tekton.TechnicalTest.Cross;

public static class DependencyInjection
{
    public static IServiceCollection AddCrossService(this IServiceCollection services)
    {
        // Register Cross
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
