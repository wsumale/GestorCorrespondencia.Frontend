using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Model;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace GestorCorrespondencia.Frontend.Functionalities.SenderConsolidation.Pages;
public partial class SenderConsolidation
{
    [Inject] DialogService DialogService { get; set; } = default!;

    bool loading = false;
    bool allowRowSelectOnRowClick = true;

    RadzenDataGrid<ConsolidatedDetail>? grid;
    private IList<ConsolidatedDetail>? selectedPackages = new List<ConsolidatedDetail>();

    private async Task OpenPreviewConsolidationAsync()
    {
        if (selectedPackages != null && selectedPackages!.Count() > 0)
        {
            Consolidated consolidated = new Consolidated();
            consolidated.RecipientLocationId = 88;
            consolidated.ConsolidatedDetail = selectedPackages;
            consolidated.Type = 1;

            await CustomDialogService.OpenPreviewNewConsolidationAsync(consolidated);
        } 
        else
        {
            await DialogService.Alert("Debe seleccionar al menos un paquete", "Error", new AlertOptions { OkButtonText="Aceptar"});
        }
    }
}
