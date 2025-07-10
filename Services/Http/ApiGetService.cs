using System.Net.Http.Headers;
using GestorCorrespondencia.Frontend.Services.Security;
using GestorCorrespondencia.Frontend.Shared.Interfaces;
using GestorCorrespondencia.Frontend.Shared.Model;

namespace GestorCorrespondencia.Frontend.Services.Http;
public class ApiGetService : IApiGetService
{
    private readonly HttpClient _httpClient;
    private readonly Uri _baseAddress;
    private readonly Uri _baseAddressUniuser;
    private readonly Uri _baseAddressUniuserWebApi;
    private readonly Uri _baseAddressUbicaciones;
    private readonly ILogger<ApiGetService> _logger;
    private readonly AccessControlService _accessControlService;
    private readonly GetCurrentUser _getCurrentUser;

    public ApiGetService(HttpClient httpClient,
                         ApiSettings settings,
                         ILogger<ApiGetService> logger,
                         AccessControlService accessControlService,
                         GetCurrentUser getCurrentUser)
    {
        _httpClient = httpClient;
        _logger = logger;
        _baseAddress = new Uri(settings.BaseAddress);
        _baseAddressUniuser = new Uri(settings.BaseAddressUniuser);
        _baseAddressUniuserWebApi = new Uri(settings.BaseAddressUniuserWebApi);
        _baseAddressUbicaciones = new Uri(settings.BaseAddressUbicaciones);
        _accessControlService = accessControlService;
        _getCurrentUser = getCurrentUser;
    }

    public async Task<HttpResponseMessage> GetAsync(string url, string accept, int source = 1, bool log = false)
    {
        try
        {
            var baseUri = source switch
            {
                1 => _baseAddress,
                2 => _baseAddressUniuser,
                3 => _baseAddressUniuserWebApi,
                4 => _baseAddressUbicaciones,
                _ => _baseAddress
            };

            var requestUri = new Uri(baseUri, url);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.UserAgent.ParseAdd("GestorCorrespondencia/1.0");

            if (accept.Length > 0)
            {
                request.Headers.Accept.ParseAdd(accept);
            }

            if (source != 2)
            {
                await _accessControlService.RefreshTokenIfExpiringAsync();
                var tokenInfo = await _getCurrentUser.GetTokenInfoAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.AccessToken);
            }

            var response = await _httpClient.SendAsync(request);
            var rawContent = await response.Content.ReadAsStringAsync();

            if (log)
            {
                _logger.LogWarning("---------- API GET RESPONSE RAW ----------");
                _logger.LogWarning("Request to: {Url}", requestUri);
                _logger.LogWarning("HTTP Status: {Code} - {Reason}", response.StatusCode, response.ReasonPhrase);
                _logger.LogWarning("Response JSON (raw):");
                _logger.LogWarning(string.IsNullOrWhiteSpace(rawContent) ? "[VACÍO]" : rawContent);
                _logger.LogWarning("-------------------------------------------");
            }

            return response;
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogWarning("La solicitud fue cancelada (posible redirección o cierre de sesión): {Message}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Excepción inesperada durante GetAsync");
            throw;
        }
    }
}