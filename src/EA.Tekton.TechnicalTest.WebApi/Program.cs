using EA.Tekton.TechnicalTest.Cross;
using EA.Tekton.TechnicalTest.Cross.Auth;
using EA.Tekton.TechnicalTest.Cross.Configuration;
using EA.Tekton.TechnicalTest.Cross.Crypto;
using EA.Tekton.TechnicalTest.Cross.Health;
using EA.Tekton.TechnicalTest.Cross.Health.Services;
using EA.Tekton.TechnicalTest.Cross.Jwt;
using EA.Tekton.TechnicalTest.Cross.Jwt.Options;
using EA.Tekton.TechnicalTest.Cross.Options;
using EA.Tekton.TechnicalTest.Domain;
using EA.Tekton.TechnicalTest.Infrastructure;
using EA.Tekton.TechnicalTest.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using EA.Tekton.TechnicalTest.Cross.MemoryCache;
using EA.Tekton.TechnicalTest.Cross.MemoryCache.Options;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var assembly = Assembly.Load(env.ApplicationName);
var versionNumber = assembly.GetName().Version?.ToString();

// Configure Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// loading appsettings.json based on environment configurations
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// add environment variables
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
// Needed to load configurations from appsettings.json
builder.Services.AddOptions();

// client IP resolvers use it
builder.Services.AddHttpContextAccessor();

// Add Cross Jwt Options
var jwtOptions = new JwtOptions();
builder.Configuration.GetSection(nameof(JwtOptions)).Bind(jwtOptions);
builder.Services.AddCrossJwtService(jwtOptions);

// Add MediatR y FluentValidation
builder.Services.AddControllers(options => options.Filters.Add<ExceptionFilter>());
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);


// Add Cross project reference
builder.Services.AddDomainService();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddCrossService();
builder.Services.AddCrossAuthService();
builder.Services.AddCrossHealthService();
builder.Services.AddCrossCryptoService();

// Add MemoryCache
var cacheOptions = new CacheOptions();
builder.Configuration.GetSection(nameof(CacheOptions)).Bind(cacheOptions);
if (cacheOptions.Enabled)
{
    builder.Services.AddMemoryCacheService();
}


// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddOptions<SwaggerGenOptions>()
    .Configure<IApiVersionDescriptionProvider>((swagger, service) =>
    {
        foreach (var description in service.ApiVersionDescriptions)
        {
            swagger.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = CrossConfiguration.ApplicationName,
                Version = $"v{versionNumber}",
                Description = CrossConfiguration.ApplicationDescription,
                Contact = new OpenApiContact
                {
                    Name = CrossConfiguration.ContactName,
                    Email = CrossConfiguration.Email
                }
            });
        }

        swagger.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = CrossConfiguration.JwtAuthorizationDescription,
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        swagger.OperationFilter<AuthorizeCheckOperationFilter>();

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        swagger.IncludeXmlComments(xmlPath);
    });

builder.Services
    .AddMvcCore()
    .AddApiExplorer();

// Register API Exception Filter
builder.Services.AddControllersWithViews(options => options.Filters.Add<ExceptionFilter>());

// Configure HTTP Strict Transport Security Protocol (HSTS)
builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(1);
});

// Register and configure CORS
var corsOptions = new CorsOptions();
builder.Configuration.GetSection(nameof(CorsOptions)).Bind(corsOptions);

builder.Services
    .AddCors(options =>
    {
        options.AddPolicy(name: corsOptions.Policy,
            policyBuilder =>
            {
                policyBuilder
                    .WithOrigins(corsOptions.Origins.ToArray())
                    .WithMethods(
                        HttpMethod.Options.ToString().ToUpper(),
                        HttpMethod.Get.ToString().ToUpper(),
                        HttpMethod.Post.ToString().ToUpper(),
                        HttpMethod.Put.ToString().ToUpper(),
                        HttpMethod.Delete.ToString().ToUpper())
                    .WithHeaders(
                        CrossConfiguration.Authorization,
                        CrossConfiguration.Accept,
                        CrossConfiguration.ContentType,
                        CrossConfiguration.Origin)
                    .AllowCredentials();
            });
    });

builder.Services.AddRouting(r =>
{
    r.LowercaseUrls = true;
    r.SuppressCheckForUnhandledSecurityMetadata = true;
});

// Register and configure API versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// Register and configure APi versioning explorer
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(corsOptions.Policy);

// Configure custom health check endpoint
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = HealthCheckService.WriterHealthCheckResponse,
    AllowCachingResponses = false
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
