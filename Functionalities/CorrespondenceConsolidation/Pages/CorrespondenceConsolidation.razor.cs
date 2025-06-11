using GestorCorrespondencia.Frontend.Functionalities.CorrespondenceConsolidation.Http;
using GestorCorrespondencia.Frontend.Functionalities.SenderConsolidation.Http;
using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.SGL;
using GestorCorrespondencia.Frontend.Shared.Components.PreviewNewConsolidation.Model;
using GestorCorrespondencia.Frontend.Shared.Model;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace GestorCorrespondencia.Frontend.Functionalities.CorrespondenceConsolidation.Pages;

public partial class CorrespondenceConsolidation
{
    [Inject] DialogService DialogService { get; set; } = default!;
    //[Inject] CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] CorrespondenceConsolidationHttp CorrespondenceConsolidationHttp { get; set; } = default!;
    [Inject] SGLService SGLService { get; set; } = default!;

    private bool loading = false;

    private IList<Location> locations = new List<Location>();
    private string selectedLocation = "";
    private int selectedLocationId = 0;

    private IList<ConsolidatedDetail> packagesDetail = new List<ConsolidatedDetail>();

    bool allowRowSelectOnRowClick = true;
    private RadzenDataGrid<ConsolidatedDetail>? grid;
    private Guid dropdownKey = Guid.NewGuid();

    private IList<ConsolidatedDetail>? _selectedPackages;
    private IList<ConsolidatedDetail> selectedPackages
    {
        get => _selectedPackages!;
        set
        {
            _selectedPackages = value;

            if (!string.IsNullOrEmpty(selectedUbicacionDestino) || _selectedPackages == null || !_selectedPackages.Any())
                return;

            selectedUbicacionDestino = _selectedPackages.First().RecipientLocation;
        }
    }

    private string? selectedUbicacionDestino;

    protected override async void OnInitialized()
    {
        loading = true;

        locations = await SGLService.GetLocationsSendConsolidatesAsync();

        loading = false;
        StateHasChanged();
    }

    private async Task GetPackagesAsync()
    {
        loading = true;

        if (selectedLocationId > 0)
        {
            selectedLocation = locations.FirstOrDefault(l => l.LocationId == selectedLocationId)!.LocationName!;
            packagesDetail = await CorrespondenceConsolidationHttp.GetPackagesPendingConsolidationByLocationIdAsync(selectedLocationId);
        }
        else
        {
            packagesDetail = new List<ConsolidatedDetail>();
        }

        selectedPackages = null;
        selectedUbicacionDestino = null;

        loading = false;
        StateHasChanged();
    }

    private void ClearSelectedLocation()
    {
        selectedUbicacionDestino = null;
        selectedPackages = null;
        selectedLocationId = 0;

        dropdownKey = Guid.NewGuid();

        packagesDetail = new List<ConsolidatedDetail>();
        StateHasChanged();
    }

    private async Task OpenPreviewConsolidation()
    {
        Consolidated consolidated = new Consolidated();
        consolidated.RecipientLocationId = selectedLocationId;
        consolidated.RecipientLocation = selectedLocation;
        consolidated.ConsolidatedDetail = selectedPackages;
        consolidated.Type = 2;

        await CustomDialogService.OpenPreviewNewConsolidation(consolidated);
    }
}
