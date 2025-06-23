using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Security.Claims;
using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components;
using Microsoft.AspNetCore.Components.Authorization;
using GestorCorrespondencia.Frontend.Functionalities.Authentication.Components;
using GestorCorrespondencia.Frontend.Functionalities.Authentication.DTO;
using GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Form;
using GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Session;
using GestorCorrespondencia.Frontend.Shared.Model;
using GestorCorrespondencia.Frontend.Shared.Interfaces;
using GestorCorrespondencia.Frontend.Functionalities.Authentication.Mapper;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.Pages;
public partial class Login
{
    [Inject] private ApiPostService ApiPostService { get; set; } = default!;
    [Inject] private DialogService DialogService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    [Inject] private IAuthCookieService AuthCookieService { get; set; } = default!;
    [Inject] private IJSRuntime JS { get; set; } = default!;
    [Inject] ILogger<Login> _logger { get; set; } = default!;

    public string subtitle = "Por favor ingresa tus credenciales para continuar.";
    public bool requiredAuthenticatorCode = false;
    private LoginForm formLogin = new();
    private LoginRequestDto login = new();
    bool busy;

    private async Task OnSubmit()
    {
        busy = true;

        login = formLogin.LoginFormToLoginRequestDto();

        try
        {
            var response = await ApiPostService.PostAsync("auth/login", login, 2, false);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                await ProcesarLoginExitoso(response, content);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(content))
                {
                    await DialogService.Alert(response.ReasonPhrase, $"Error código ({(int)response.StatusCode})", new AlertOptions() { OkButtonText = "Aceptar" });
                }

                var errorObject = JsonSerializer.Deserialize<ApiResponseNotAcceptable>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (errorObject!.Type != "TwoFactorAuthenticationRequiredException")
                {
                    var statusDialog = await DialogService.OpenAsync<ViewErrorsModal>(
                        errorObject!.Title,
                        new Dictionary<string, object>
                            { { "responseNotAcceptable", errorObject } },
                        new DialogOptions
                            { Width = "400px", CloseDialogOnEsc = false, CloseDialogOnOverlayClick = false, ShowClose = false });

                    if (statusDialog != null && errorObject.Type == "TwoFactorAuthenticationConfigurationRequiredException")
                    {
                        await Configure2FA();
                    }
                }

                if (errorObject.Type == "TwoFactorAuthenticationRequiredException")
                {
                    requiredAuthenticatorCode = true;
                    subtitle = "Ingrese el código de autenticación para ingresar al sistema.";
                }

            }
        }
        catch (Exception e)
        {
            await DialogService.Alert(e.Message, "Error de Conexión", new AlertOptions() { OkButtonText = "Aceptar" });
        }
        finally
        {
            busy = false;
        }
    }

    private async Task ProcesarLoginExitoso(HttpResponseMessage response, string content)
    {
        var token = response.Headers.GetValues("Authorization").FirstOrDefault()?.Replace("Bearer ", "");
        var user = JsonSerializer.Deserialize<SessionUser>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var cookieRefreshToken = await AuthCookieService.GetAuthCookie(response);

        if (string.IsNullOrEmpty(token) || user == null)
        {
            await DialogService.Alert("No se pudo procesar el inicio de sesión", "Error de autenticación", new AlertOptions { OkButtonText = "Aceptar" });
            return;
        }

        var userJson = JsonSerializer.Serialize(user);

        await JS.InvokeVoidAsync("auth.setTempSession", token, userJson);
        await JS.InvokeVoidAsync("auth.setTempRefreshToken", cookieRefreshToken!.RefreshToken, cookieRefreshToken.Expires?.ToString("o"));

        NavigationManager.NavigateTo("/auth/start-session", forceLoad: true);
    }


    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        string _exp = user.FindFirstValue("exp") ?? "";

        if (DateTime.TryParse(_exp, out var expDateTime))
        {
            if (user.Identity?.IsAuthenticated == true || expDateTime >= DateTime.UtcNow)
            {
                NavigationManager.NavigateTo("/principal/home", forceLoad: true);
            }
        }
    }

    private async Task Configure2FA()
    {
        await DialogService.OpenAsync<Form2FA>(
            "Configuración de 2FA",
            new Dictionary<string, object>
                { { "login",  login } },
            new DialogOptions
                { Width = "75%", CssClass = "dialog-2fa" });
    }

    public void returnToLogin()
    {
        subtitle = "Por favor ingresa tus credenciales para continuar.";
        requiredAuthenticatorCode = false;
        login = new();
        formLogin = new();
    }

}