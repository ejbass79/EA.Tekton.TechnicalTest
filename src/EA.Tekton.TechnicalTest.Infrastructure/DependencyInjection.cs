using EA.Tekton.TechnicalTest.Cross.Auth.Infrastructure.DataAccess.Entities;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Configurations;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Context;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Interfaces;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Repository;
using EA.Tekton.TechnicalTest.Infrastructure.DataAccess.UnitOfWork;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Diagnostics;

using IdentityOptions = EA.Tekton.TechnicalTest.Cross.Options.IdentityOptions;

namespace EA.Tekton.TechnicalTest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        //Context SQL Server
        services.ConfigureOptions<DataBaseOptionsSetup>();

        services.AddDbContext<EATektonTechnicalTestContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            var dataBaseOptions = serviceProvider.GetService<IOptions<DataBaseOptions>>()!.Value;

            dbContextOptionsBuilder.UseSqlServer(dataBaseOptions.ConnectionString, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.EnableRetryOnFailure(dataBaseOptions.MaxRetryCount);

                sqlServerOptionsAction.CommandTimeout(dataBaseOptions.CommandTimeout);
            });

            dbContextOptionsBuilder.EnableDetailedErrors(dataBaseOptions.EnableDetailedErrors);

            dbContextOptionsBuilder.EnableSensitiveDataLogging(dataBaseOptions.EnableSensitiveDataLogging);

            dbContextOptionsBuilder.LogTo(message =>
            {
                Debug.WriteLine(message);
            }, LogLevel.Information);
        });

        var identityOptionsConfig = new IdentityOptions();
        configuration.GetSection(nameof(IdentityOptions)).Bind(identityOptionsConfig);

        services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = identityOptionsConfig.RequireNonAlphanumeric;
                options.Password.RequiredLength = identityOptionsConfig.RequiredLength;
                options.Password.RequireDigit = identityOptionsConfig.RequiredDigit;
                options.Password.RequireLowercase = identityOptionsConfig.RequireLowercase;
                options.Password.RequiredUniqueChars = identityOptionsConfig.RequiredUniqueChars;
                options.Password.RequireUppercase = identityOptionsConfig.RequireUppercase;
                options.Lockout.MaxFailedAccessAttempts = identityOptionsConfig.MaxFailedAttempts;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(identityOptionsConfig.LockoutTimeSpanInDays);
                options.Lockout.AllowedForNewUsers = identityOptionsConfig.AllowedForNewUsers;
                options.SignIn.RequireConfirmedEmail = identityOptionsConfig.RequireConfirmedEmail;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<EATektonTechnicalTestContext>()
            .AddDefaultTokenProviders();

        services.Configure<PasswordHasherOptions>(option =>
        {
            option.IterationCount = identityOptionsConfig.IterationCount;
        });

        services.AddDbContext<DbContext, EATektonTechnicalTestContext>();
        services.AddDbContext<IDataContext, EATektonTechnicalTestContext>();
        services.AddScoped<ILogger, Logger<EATektonTechnicalTestContext>>();

        services.AddTransient<IRepositoryFactory, RepositoryFactory>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}