using GestorCorrespondencia.Frontend.Functionalities.AllPackages.Http;
using GestorCorrespondencia.Frontend.Functionalities.AllPackages.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.SGL;
using GestorCorrespondencia.Frontend.Services.SGU;
using GestorCorrespondencia.Frontend.Shared.Constants;
using GestorCorrespondencia.Frontend.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.AllPackages.Pages;
public partial class AllPackages
{
    [Inject] AllPackagesHttp AllPackagesHttp { get; set; } = default!;
    [Inject] SGLService SGLService { get; set; } = default!;
    [Inject] SGUService SGUService { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    private bool loading = false;
    private IList<AllPackagesView> packages = new List<AllPackagesView>();

    private IList<Location>? Locations;
    private IList<User>? SenderUsers;
    private IList<User>? ReceiverUsers;
    private IEnumerable<object> States => Enum.GetValues(typeof(State))
    .Cast<State>()
    .Select(e => new { Value = (int)e, Description = e.Description() });

    private AllPackagesFilterForm filterForm = new();

    protected override async Task OnInitializedAsync()
    {
        loading = true;
        Locations = await SGLService.GetLocationsSendPackagesAsync();
        loading = false;
        StateHasChanged();
    }

    private async Task GetSenderUsersAsync()
    {
        loading = true;
        SenderUsers = await SGUService.GetUsersByLocationAsync(filterForm!.SenderLocationId, false);
        loading = false;
        StateHasChanged();
    }
    private async Task GetReceiverUsersAsync()
    {
        loading = true;
        ReceiverUsers = await SGUService.GetUsersByLocationAsync(filterForm!.ReceiverLocationId, false);
        loading = false;
        StateHasChanged();
    }

    private async Task LoadDataAsync()
    {
        loading = true;
        packages = await AllPackagesHttp.GetAllPackagesAsync(filterForm);
        loading = false;
        StateHasChanged();
    }

    private async Task DownloadReportPDF()
    {
        loading = true;
        await AllPackagesHttp.GetAllPackagesDownloadAsync(filterForm, "application/pdf");
        loading = false;
        StateHasChanged();
    }

    private async Task DownloadReportCSV()
    {
        loading = true;
        await AllPackagesHttp.GetAllPackagesDownloadAsync(filterForm, "application/csv");
        loading = false;
        StateHasChanged();
    }

    private void ClearFilters()
    {
        filterForm = new();
        packages = new List<AllPackagesView>();
    }
}