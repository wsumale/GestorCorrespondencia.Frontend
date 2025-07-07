using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Http;
using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Pages;
public partial class PackagesWithIncidence
{
    [Inject] PackagesWithIncidentHttp PackagesWithIncidentHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    private bool loading = false;

    private IList<PackageWithIncident> packages = new List<PackageWithIncident>();

    protected override async void OnInitialized()
    {
        loading = true;

        await LoadDataAsync();
        base.OnInitialized();

        loading = false;
        StateHasChanged();
    }

    private async Task LoadDataAsync()
    {
        packages = await PackagesWithIncidentHttp.GetPackagesWithIncident();
    }

    private async Task OpenIncidentDetailAsync(int IncidentId)
    {
        await CustomDialogService.OpenIncidentDetail(IncidentId);
    }

    private async Task OpenPackageDetailAsync(int? PackageId)
    {
        await CustomDialogService.OpenViewPackageNullable(PackageId);
    }

    private async Task OpenResolveIncidentAsync(int IncidentId, int? IncidentTypeId)
    {
        await CustomDialogService.OpenResolveIncidentAsync(IncidentId, IncidentTypeId);
    }

    bool? solvedFilter;

    void OnSolvedFilterChanged(object value)
    {
        solvedFilter = value as bool?;
        // Aquí, si usas LoadData, puedes disparar el filtrado en el backend
        // Si el grid es local, Radzen lo hace automático con FilterProperty
    }

}