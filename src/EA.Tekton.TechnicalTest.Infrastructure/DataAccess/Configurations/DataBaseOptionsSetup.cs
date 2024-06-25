using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EA.Tekton.TechnicalTest.Infrastructure.DataAccess.Configurations;

internal class DataBaseOptionsSetup(IConfiguration configuration) : IConfigureOptions<DataBaseOptions>
{
    private const string ConfigurationSectionName = nameof(DataBaseOptions);

    public void Configure(DataBaseOptions options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
