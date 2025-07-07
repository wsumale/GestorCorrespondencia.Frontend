using GestorCorrespondencia.Frontend.Functionalities.PendingPackages.Http;
using GestorCorrespondencia.Frontend.Functionalities.PendingPackages.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.PendingPackages.Pages;
public partial class PendingPackages
{
    [Inject] PendingPackagesHttp PendingPackagesHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] DialogService DialogService { get; set; } = default!;

    private bool loading = false;
    private IList<PendingPackage> packages = new List<PendingPackage>();

    protected override async Task OnInitializedAsync()
    {
        loading = true;
        await LoadDataAsync();
        loading = false;

        StateHasChanged();
    }

    private async Task LoadDataAsync()
    {
        packages = await PendingPackagesHttp.GetPendingPackagesAsync();
    }

    private async Task RefreshLoadDataAsync()
    {
        loading = true;
        await LoadDataAsync();
        loading = false;
        StateHasChanged();
    }

    private async Task ReceivePackageAsync(int PackageId)
    {
        bool confirm = await CustomDialogService.OpenConfirmAsync("¿Desea recibir este paquete?", "Confirmar", "Recibir", "Cancelar", new DialogOptions { Width = "400px" });

        if (confirm == true)
        {
            StateHasChanged();
            loading = true;
            await PendingPackagesHttp.ReceiveAsync(PackageId);
            loading = false;
            await RefreshLoadDataAsync();
        } 
        else
        {
            DialogService.Close();
        }
    }

    private async Task CreateIncidentDialogAsync(int PackageId, int ConsolidatedDetailId)
    {
        //await CustomDialogService.OpenCreatePackageIncidentAsync(PackageId, ConsolidatedDetailId);
        await CustomDialogService.OpenChangeReceiverInDestinationAsync(PackageId);
        await RefreshLoadDataAsync();
    }
}