using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Shared.Components.ViewConsolidationDetail.Http;
using GestorCorrespondencia.Frontend.Shared.Components.ViewConsolidationDetail.Model;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Shared.Components.ViewConsolidationDetail.Components;
public partial class ViewConsolidationDetail
{
    [Inject] ViewConsolidationDetailHttp ViewConsolidationDetailHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    [Parameter] public int ConsolidationId { get; set; }
    [Parameter] public bool CurrentUser { get; set; }

    private Consolidation consolidation = new();

    private bool loading = false;

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
        consolidation = await ViewConsolidationDetailHttp.GetConsolidationDetailAsync(ConsolidationId, CurrentUser);
    }
}