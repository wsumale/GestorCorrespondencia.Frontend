using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace GestorCorrespondencia.Frontend.Functionalities.CorrespondenceConsolidation.Pages;
public partial class CorrespondenceConsolidation
{
    [Inject] DialogService DialogService { get; set; } = default!;
    [Inject] CustomDialogService PackageDialogService { get; set; } = default!;

    bool allowRowSelectOnRowClick = true;

    RadzenDataGrid<Package>? grid;

    private IList<Package>? _selectedPackages;
    private IList<Package> selectedPackages
    {
        get => _selectedPackages!;
        set
        {
            _selectedPackages = value;

            // Solo si no hay una selección previa en el dropdown
            if (!string.IsNullOrEmpty(selectedUbicacionDestino) || _selectedPackages == null || !_selectedPackages.Any())
                return;

            // Asignar la ubicación de la primera fila seleccionada al dropdown
            selectedUbicacionDestino = _selectedPackages.First().UbicacionDestino;
        }
    }

    private IEnumerable<Package> filteredPackages => 
        string.IsNullOrWhiteSpace(selectedUbicacionDestino) ? packages : packages.Where(p => p.UbicacionDestino == selectedUbicacionDestino);

    private string? selectedUbicacionDestino;

    private void ClearSelectedLocation()
    {
        selectedUbicacionDestino = null;
        StateHasChanged();
    }

    private async Task OpenPreviewConsolidation()
    {
        //await CustomDialogService.OpenPreviewNewConsolidation(selectedPackages!);
    }
}