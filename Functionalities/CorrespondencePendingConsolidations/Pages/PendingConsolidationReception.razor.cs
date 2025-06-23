using GestorCorrespondencia.Frontend.Functionalities.CorrespondencePendingConsolidations.Http;
using GestorCorrespondencia.Frontend.Functionalities.CorrespondencePendingConsolidations.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.CorrespondencePendingConsolidations.Pages;
public partial class PendingConsolidationReception
{
    [Inject] private CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] private CorrespondencePendingConsolidationHttp CorrespondencePendingConsolidationHttp { get; set; } = default!;

    private bool loading = false;
    private IList<PendingConsolidationForCorrespondenceModel> consolidates = new List<PendingConsolidationForCorrespondenceModel>();

    protected override async void OnInitialized()
    {
        loading = true;

        await LoadDataAsync();
        base.OnInitialized();

        loading = false;
        StateHasChanged();
    }

    private async Task ConsolidatedReceptionListAsync(int ConsolidatedId)
    {
        await CustomDialogService.OpenConsolidatedReceptionListAsync(ConsolidatedId);
        await RefreshLoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        consolidates = await CorrespondencePendingConsolidationHttp.GetPendingConsolidationsForCorrespondenceAsync();
    }

    private async Task RefreshLoadDataAsync()
    {
        loading = true;
        await LoadDataAsync();
        loading = false;
        StateHasChanged();
    }
}