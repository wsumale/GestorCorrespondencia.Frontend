using GestorCorrespondencia.Frontend.Functionalities.CorrespondencePendingConsolidations.Model;
using GestorCorrespondencia.Frontend.Functionalities.ReceptionPendingConsolidations.Http;
using GestorCorrespondencia.Frontend.Functionalities.ReceptionPendingConsolidations.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.ReceptionPendingConsolidations.Pages;
public partial class PendingConsolidationReception
{
    [Inject] private CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] private ReceptionPendingConsolidationsHttp ReceptionPendingConsolidationsHttp { get; set; } = default!;

    private bool loading = false;
    private IList<PendingConsolidationForReceptionModel> consolidates = new List<PendingConsolidationForReceptionModel>();

    protected override async void OnInitialized()
    {
        loading = true;

        await LoadData();
        base.OnInitialized();

        loading = false;
        StateHasChanged();
    }

    private async Task ConsolidatedReceptionListAsync(int ConsolidatedId)
    {
        await CustomDialogService.OpenConsolidatedReceptionListAsync(ConsolidatedId);
    }

    private async Task LoadData()
    {
        consolidates = await ReceptionPendingConsolidationsHttp.GetPendingConsolidationsForReceptionAsync();
    }
}