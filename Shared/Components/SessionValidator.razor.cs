using System.Security.Claims;
using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Services.Security;
using GestorCorrespondencia.Frontend.Services.UI;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components;
public partial class SessionValidator
{
    [Inject] AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
    [Inject] NavigationManager Navigation { get; set; } = default!;
    [Inject] DialogService DialogService { get; set; } = default!;
    [Inject] MainLayoutService MainLayoutService { get; set; } = default!;
    [Inject] AccessControlService AccessControlService { get; set; } = default!;
    [Inject] ApiHeadAccessService ApiHeadAccessService { get; set; } = default!;
    [Inject] ILogger<SessionValidator> _logger { get; set; } = default!;
    [Inject] IJSRuntime JSRuntime { get; set; } = default!;
    [Parameter] public string? title { get; set; }
    [Parameter] public string? path { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; } = null!;

    private ClaimsPrincipal? user;
    private bool _initialized;
    private string _exp = string.Empty;
    private string _expRefreshToken = string.Empty;
    private string _urlCloseSession = "/auth/logout";
    private bool _hasAccess = false;
    private bool _notHasAccess = false;
    private bool _loading = false;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
            _loading = true; 

        if (firstRender && !_initialized)
        {
            _initialized = true;

            var reason = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "SessionReason");

            if (!string.IsNullOrEmpty(reason))
            {
                if (reason == "expired")
                {
                    await DialogService.Alert("Tu sesión ha expirado. Por favor, vuelve a iniciar sesión.", "Sesión Expirada");
                }
                else if (reason == "manual")
                {
                    await DialogService.Alert("Has cerrado sesión correctamente.", "Sesión Finalizada");
                }

                await JSRuntime.InvokeVoidAsync("localStorage.removeItem", "SessionReason");

                Navigation.NavigateTo("/", forceLoad: true);
                return;
            }

            await ValidatorAsync();

            _loading = false;

            StateHasChanged();
        }
    }

    protected override async void OnInitialized()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        user = authState.User;
        _exp = user.FindFirstValue("exp") ?? "";
        _expRefreshToken = user.FindFirstValue("expRefreshToken") ?? "";

        if (user.Identity?.IsAuthenticated == true)
        {
            MainLayoutService.UpdateHeader(title!);
            MainLayoutService.UpdateUserInfo(
                user.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario",
                user.FindFirst("EmployeeCode")?.Value ?? "------"
            );
            MainLayoutService.UpdateUserInfoSidebar(
                user.FindFirst(ClaimTypes.Name)?.Value ?? "Usuario",
                user.FindFirst(ClaimTypes.Role)?.Value ?? "Usuario"
            );
        }

        base.OnInitialized();
    }

    private async Task ValidatorAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (DateTime.TryParse(_expRefreshToken, out var expDateTime))
        {
            if ((!user.Identity?.IsAuthenticated ?? true) || expDateTime <= DateTime.UtcNow)
            {
                await CloseSessionAsync("Por favor, inicia sesión nuevamente.", "Sesión expirada");
            }
            else
            {
                await ValidateAccessPathAsync(path!);
            }
        }
        else
        {
            await CloseSessionAsync("No se pudo validar la sesión.", "Error");
        }
    }

    private async Task ValidateAccessPathAsync(string path)
    {
        var response = await ApiHeadAccessService.HeadAsync("auth/frontend-path", path, 2, true);

        _hasAccess = response.IsSuccessStatusCode ? true : false;
        _notHasAccess = !response.IsSuccessStatusCode ? true : false;
        StateHasChanged();
    }

    private async Task CloseSessionAsync(string msg, string title)
    {
        await JSRuntime.InvokeVoidAsync("localStorage.setItem", "SessionReason", "expired");
        Navigation.NavigateTo(_urlCloseSession, forceLoad: true);
    }
}