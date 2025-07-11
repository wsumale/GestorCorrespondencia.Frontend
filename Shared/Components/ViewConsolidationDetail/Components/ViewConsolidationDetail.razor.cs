using GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Http;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Model;
using GestorCorrespondencia.Frontend.Shared.Components.ViewConsolidationDetail.Http;
using GestorCorrespondencia.Frontend.Shared.Components.ViewConsolidationDetail.Model;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Shared.Components.ViewConsolidationDetail.Components;
public partial class ViewConsolidationDetail
{
    [Inject] ViewConsolidationDetailHttp ViewConsolidationDetailHttp { get; set; } = default!;
    [Inject] ConsolidationTrackingHttp ConsolidationTrackingHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    [Parameter] public int ConsolidationId { get; set; }
    [Parameter] public bool CurrentUser { get; set; }

    private Consolidation consolidation = new();

    private bool loading = false;

    protected override async Task OnInitializedAsync()
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

    private async Task GetConsolidatedTracking()
    {
        loading = true;
        await ConsolidationTrackingHttp.GetConsolidatedTrackingDownloadAsync(consolidation!.ConsolidatedId, true);
        loading = false;
    }

    private async Task GetConsolidatedPackages()
    {
        loading = true;
        await ConsolidationTrackingHttp.GetConsolidatedPackagesDownloadAsync(consolidation!.ConsolidatedId, true);
        loading = false;
    }
}