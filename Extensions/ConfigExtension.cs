using GestorCorrespondencia.Frontend.Shared.Model;

namespace GestorCorrespondencia.Frontend.Extensions;
public static class ConfigExtension
{
    public static void AddConfigureClient(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettingsSection = configuration.GetSection("ApiSettings");

        services.Configure<ApiSettings>(apiSettingsSection);
        services.AddSingleton(apiSettingsSection.Get<ApiSettings>()!);

        services.AddScoped(sp =>
        {
            HttpClientHandler handler;

#if DEBUG
            handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
#else
            handler = new HttpClientHandler();
#endif

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("GestorCorrespondencia/1.0");
            return client;
        });
    }
}