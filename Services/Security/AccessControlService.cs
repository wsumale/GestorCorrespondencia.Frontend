using System.Security.Claims;
using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Session;
using GestorCorrespondencia.Frontend.Shared.Model;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;
using Radzen;
using GestorCorrespondencia.Frontend.Shared.Interfaces;
using GestorCorrespondencia.Frontend.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using GestorCorrespondencia.Frontend.Services.Http;
using Microsoft.JSInterop;

namespace GestorCorrespondencia.Frontend.Services.Security;
public class AccessControlService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _accessor;
    private readonly Uri _baseAddress;
    private readonly Uri _baseAddressUniuser;
    private readonly GetCurrentUser _getCurrentUser;
    private readonly DialogService _dialogService;
    private readonly NavigationManager _navigationManager;
    private readonly CustomDialogService _customDialogService;
    private readonly IAuthCookieService _authCookieService;
    private readonly AuthSessionService _authSessionService;
    private readonly ISessionReasonService _sessionReasonService;
    private readonly AuthenticationStateProvider _authProvider;
    private readonly IJSRuntime _jsRuntime;
    private readonly ILogger _logger;

    public AccessControlService(HttpClient httpClient,
                                IHttpContextAccessor accessor,
                                ApiSettings settings,
                                GetCurrentUser getCurrentUser,
                                DialogService dialogService,
                                NavigationManager navigationManager,
                                CustomDialogService customDialogService,
                                IAuthCookieService authCookieService,
                                AuthSessionService authSessionService,
                                ISessionReasonService sessionReasonService,
                                AuthenticationStateProvider authProvider,
                                IJSRuntime jsRuntime,
                                ILogger<AccessControlService> logger)
    {
        _httpClient = httpClient;
        _accessor = accessor;
        _baseAddress = new Uri(settings.BaseAddress);
        _baseAddressUniuser = new Uri(settings.BaseAddressUniuser);
        _getCurrentUser = getCurrentUser;
        _dialogService = dialogService;
        _navigationManager = navigationManager;
        _customDialogService = customDialogService;
        _authCookieService = authCookieService;
        _authSessionService = authSessionService;
        _sessionReasonService = sessionReasonService;
        _authProvider = authProvider;
        _jsRuntime = jsRuntime;
        _logger = logger;
    }

    public async Task RenewAccessTokenAsync()
    {
        try
        {
            var tokenInfo = await _getCurrentUser.GetTokenInfoAsync();
            string accessToken = tokenInfo.AccessToken!;
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(accessToken);
            var exp = DateTime.SpecifyKind(jwt.ValidTo, DateTimeKind.Utc);

            string refreshToken = tokenInfo.RefreshToken!;
            string expRefreshToken = tokenInfo.expRefreshToken!;

            var requestUri = new Uri(_baseAddressUniuser, "auth/refresh-token");
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Headers.Add("Cookie", $"refresh_token={refreshToken}");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var newAccessToken = response.Headers.GetValues("Authorization").FirstOrDefault()?.Replace("Bearer ", "");
                var newHandler = new JwtSecurityTokenHandler();
                var newJwt = newHandler.ReadJwtToken(newAccessToken);
                var expAccessToken = DateTime.SpecifyKind(newJwt.ValidTo, DateTimeKind.Utc);


                var newRefreshTokenCookie = await _authCookieService.GetAuthCookie(response);
                string newRefreshToken = newRefreshTokenCookie!.RefreshToken;
                string newExpRefreshToken = newRefreshTokenCookie.Expires?.ToString("o")!;

                var user = _accessor.HttpContext!.User.ReconstructUserFromClaims();

                var payload = new RefreshSessionPayload
                {
                    AccessToken = newAccessToken!,
                    RefreshToken = newRefreshToken,
                    RefreshTokenExp = newExpRefreshToken,
                    User = user
                };

                await _jsRuntime.InvokeVoidAsync("auth.refreshSession", JsonSerializer.Serialize(payload));

                var baseUri = new Uri(_navigationManager.BaseUri);
                var endpoint = new Uri(baseUri, "auth/refresh-session");
                var responseNewToken = await _httpClient.PostAsJsonAsync(endpoint, payload);

                if (responseNewToken.IsSuccessStatusCode)
                {
                    var newClaims = new List<Claim>
                    {
                        new Claim("EmployeeCode", user.EmployeeCode),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.User),
                        new Claim(ClaimTypes.Role, user.Rol),
                        new Claim("LocationId", user.LocationId?.ToString() ?? ""),
                        new Claim("StoreId", user.StoreId?.ToString() ?? ""),
                        new Claim("Store", user.Store ?? ""),
                        new Claim("AccessToken", newAccessToken!),
                        new Claim("RefreshToken", newRefreshToken),
                        new Claim("exp", expAccessToken.ToString("o")),
                        new Claim("expRefreshToken", newExpRefreshToken)
                    };

                    foreach (var menu in user.Menu!)
                    {
                        var json = JsonSerializer.Serialize(menu);
                        newClaims.Add(new Claim($"Menu:{menu.Path}", json));
                    }

                    var identity = new ClaimsIdentity(newClaims, "CustomAuth");
                    var principal = new ClaimsPrincipal(identity);

                    if (_authProvider is CustomAuthenticationStateProvider customProvider)
                    {
                        customProvider.UpdateAuthenticationState(principal);
                    }
                }
                else
                {
                    await _customDialogService.OpenViewErrorsAsync(responseNewToken);
                    await CloseSessionAsync();
                    _logger.LogWarning("Error al actualizar sesión");
                    _logger.LogWarning(accessToken);
                    _logger.LogWarning(exp.ToString("o"));
                    _logger.LogWarning(refreshToken);
                    _logger.LogWarning(expRefreshToken);
                }

            }
            else
            {
                await _customDialogService.OpenViewErrorsAsync(response);
                await CloseSessionAsync();
                _logger.LogWarning("Error al actualizar el token");
                _logger.LogWarning(accessToken);
                _logger.LogWarning(exp.ToString("o"));
                _logger.LogWarning(refreshToken);
                _logger.LogWarning(expRefreshToken);
            }
        } catch (Exception e)
        {
            await _dialogService.Alert(e.Message, "Error interno", new AlertOptions() { OkButtonText = "Aceptar" });
            await CloseSessionAsync();
        }

    }

    public async Task RefreshTokenIfExpiringAsync()
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            await CloseSessionAsync();
        }

        string _accessToken = user.FindFirstValue("AccessToken") ?? "";
        string _exp = user.FindFirstValue("exp") ?? "";

        string _refreshToken = user.FindFirstValue("RefreshToken") ?? "";
        string _expRefreshToken = user.FindFirstValue("expRefreshToken") ?? "";

        var dateTimeStyle = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal;

        if (DateTime.TryParse(_exp, CultureInfo.InvariantCulture, dateTimeStyle, out var accessTokenExp) &&
            DateTime.TryParse(_expRefreshToken, CultureInfo.InvariantCulture, dateTimeStyle, out var refreshTokenExp))
        {
            var now = DateTime.UtcNow;

            if (accessTokenExp <= now && refreshTokenExp > now)
            {
                _logger.LogWarning("Renovar token");
                _logger.LogWarning($"{accessTokenExp.ToString()} * {refreshTokenExp.ToString()} * {now.ToString()}");
                await RenewAccessTokenAsync();
            }
            else if (accessTokenExp <= now && refreshTokenExp <= now)
            {
                await CloseSessionAsync();
                _logger.LogWarning("Cerrar sesión");
            } else
            {
                _logger.LogWarning("Token activo");
                _logger.LogWarning($"{accessTokenExp.ToString()} * {refreshTokenExp.ToString()} * {now.ToString()}");
            }
        }
        else
        {
            _logger.LogError("No se pudieron interpretar los valores de expiración de los tokens (formato de fecha inválido).");
            await CloseSessionAsync();
        }

    }

    public async Task CloseSessionAsync()
    {
        await _sessionReasonService.SetSessionReasonAsync("expired");
        _authSessionService.CloseSession();
    }
}