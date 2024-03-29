using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

namespace MamisSolidarias.WebAPI.TEMPLATE.Extensions;

internal static class DataProtectionExtensions
{
    public static void AddDataProtection(this IServiceCollection services, IConfiguration configuration)
    {
        var dataProtectionKeysPath = configuration.GetValue<string>("DataProtectionKeysPath");
        if (!string.IsNullOrWhiteSpace(dataProtectionKeysPath))
        {
            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(dataProtectionKeysPath))
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });
        }
    }
}