using EA.Tekton.TechnicalTest.Cross.Crypto.Interfaces;
using EA.Tekton.TechnicalTest.Cross.Crypto.Options;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace EA.Tekton.TechnicalTest.Cross.Crypto.Services;

public class EncryptionService : IEncryptionService
{
    private readonly IDataProtector _dataProtector;
    private readonly CryptoOptions _cryptoOptions = new();
    private readonly ILogger<EncryptionService> _logger;

    public EncryptionService(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration, ILogger<EncryptionService> logger)
    {
        configuration.GetSection(nameof(CryptoOptions)).Bind(_cryptoOptions);

        _dataProtector = dataProtectionProvider.CreateProtector($"{_cryptoOptions.DataProtectionSecret}");

        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public string Protect(string plainText)
    {
        return _dataProtector.Protect(plainText);
    }

    public string Unprotect(string cipherText)
    {
        return _dataProtector.Unprotect(cipherText);
    }

    public string? ProtectToTimeLimited(string plainText, double timeLifeHours = 0)
    {
        try
        {
            var protectorTime = _dataProtector.ToTimeLimitedDataProtector();

            return protectorTime.Protect(plainText, TimeSpan.FromHours(timeLifeHours == 0 ? _cryptoOptions.DataProtectionTimeLifeHours : timeLifeHours));
        }
        catch (Exception exception)
        {
            _logger.LogError("Error: {@Exception}", exception);

            return null;
        }
    }

    public (bool isSuccess, string message) UnprotectToTimeLimited(string cipherText)
    {
        try
        {
            var protectorTime = _dataProtector.ToTimeLimitedDataProtector();

            return (isSuccess: true, message: protectorTime.Unprotect(cipherText));
        }
        catch (Exception exception)
        {
            _logger.LogError("Error: {@Exception}", exception);

            return (isSuccess: false, message: exception.Message);
        }
    }
}
