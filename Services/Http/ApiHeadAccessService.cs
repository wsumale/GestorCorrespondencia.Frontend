using GestorCorrespondencia.Frontend.Services.Security;
using GestorCorrespondencia.Frontend.Shared.Model;
using System.Net.Http.Headers;

namespace GestorCorrespondencia.Frontend.Services.Http;
public class ApiHeadAccessService
{
    private readonly HttpClient _httpClient;
    private readonly Uri _baseAddress;
    private readonly Uri _baseAddressUniuser;
    private readonly Uri _baseAddressUniuserWebApi;
    private readonly Uri _baseAddressUbicaciones;
    private readonly ILogger<ApiHeadAccessService> _logger;
    private readonly AccessControlService _accessControlService;
    private readonly GetCurrentUser _getCurrentUser;

    public ApiHeadAccessService(HttpClient httpClient,
                          ApiSettings settings,
                          ILogger<ApiHeadAccessService> logger,
                          AccessControlService accessControlService,
                          GetCurrentUser getCurrentUser)
    {
        _httpClient = httpClient;
        _baseAddress = new Uri(settings.BaseAddress);
        _baseAddressUniuser = new Uri(settings.BaseAddressUniuser);
        _baseAddressUniuserWebApi = new Uri(settings.BaseAddressUniuserWebApi);
        _baseAddressUbicaciones = new Uri(settings.BaseAddressUbicaciones);
        _logger = logger;
        _accessControlService = accessControlService;
        _getCurrentUser = getCurrentUser;
    }

    public async Task<HttpResponseMessage> HeadAsync(string url, int source = 1, bool log = false, bool refresh = false)
    {
        try { 
            var baseUri = source switch
            {
                1 => _baseAddress,
                2 => _baseAddressUniuser,
                3 => _baseAddressUniuserWebApi,
                4 => _baseAddressUbicaciones,
                _ => _baseAddress
            };

            var requestUri = new Uri(baseUri, url);
            var request = new HttpRequestMessage(HttpMethod.Head, requestUri);
            request.Headers.UserAgent.ParseAdd("GestorCorrespondencia/1.0");

            if (source == 1)
            {
                await _accessControlService.RefreshTokenIfExpiringAsync();
                var tokenInfo = await _getCurrentUser.GetTokenInfoAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.AccessToken);
            }

            var response = await _httpClient.SendAsync(request);
            var rawContent = await response.Content.ReadAsStringAsync();

            if (log)
            {
                _logger.LogWarning("---------- API HEAD RESPONSE RAW ----------");
                _logger.LogWarning("Request to: {Url}", requestUri);
                _logger.LogWarning("HTTP Status: {Code} - {Reason}", response.StatusCode, response.ReasonPhrase);
                _logger.LogWarning("Response JSON (raw):");
                _logger.LogWarning(string.IsNullOrWhiteSpace(rawContent) ? "[VACÍO]" : rawContent);
                _logger.LogWarning("-------------------------------------------");
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Excepción inesperada durante HeadAsync");
            throw;
        }
    }
}