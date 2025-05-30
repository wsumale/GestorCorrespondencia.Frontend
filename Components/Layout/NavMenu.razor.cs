using GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Session;
using GestorCorrespondencia.Frontend.Services.Security;
using GestorCorrespondencia.Frontend.Services.UI;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Components.Layout;
public partial class NavMenu
{
    [Inject] MainLayoutService LayoutService { get; set; } = default!;
    [Inject] ILogger<NavMenu> _logger { get; set; } = default!;
    [Inject] GetCurrentUser GetCurrentUser { get; set; } = default!;

    private bool sidebarExpanded;
    private string _searchModule = string.Empty;
    public string name { get; set; } = "Usuario";
    public string rol { get; set; } = "Usuario";

    protected override void OnInitialized()
    {
        sidebarExpanded = LayoutService.GetSidebarExpanded();
        LayoutService.OnSidebarExpandedChanged += OnSidebarStateChanged;
        LayoutService.OnSidebarUserNameChanged += async (nuevoTexto) =>
        {
            name = nuevoTexto;
            await InvokeAsync(StateHasChanged);
        };
        LayoutService.OnSidebarRolChanged += async (nuevoTexto) =>
        {
            rol = nuevoTexto;
            await InvokeAsync(StateHasChanged);
        };
    }

    private void OnSidebarStateChanged(bool expanded)
    {
        sidebarExpanded = expanded;
        InvokeAsync(StateHasChanged);
    }

    private void SearchModule(ChangeEventArgs args)
    {
        _searchModule = args.Value!.ToString()!;
    }
    private List<Menu> menus = new();

    protected override async Task OnInitializedAsync()
    {
        menus = await GetCurrentUser.GetMenusAsync();
    }
}