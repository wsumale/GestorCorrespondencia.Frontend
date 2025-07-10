using GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Http;
using GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Pages;
public partial class CorrespondenceConsolidationTracking
{
    [Inject] ConsolidationTrackingHttp ConsolidationTrackingHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    [Parameter] public int? ConsolidationIdParam { get; set; }

    private bool loading = false;
    private int? ConsolidationId;
    private bool foundConsolidated;
    private bool firstSearch = false;

    private ConsolidationTrackingView? consolidated = new();

    private async Task SearchConsolidationAsync()
    {
        loading = true;
        consolidated = await ConsolidationTrackingHttp.GetConsolidationDetailAsync(ConsolidationId ?? 0, false);
        foundConsolidated = consolidated != null && consolidated.ConsolidatedId > 0;
        firstSearch = true;
        loading = false;
        StateHasChanged();
    }
}