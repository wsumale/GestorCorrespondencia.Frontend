using GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Http;
using GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Components;
public partial class ConsolidationTrackingDetail
{
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] ConsolidationTrackingHttp ConsolidationTrackingHttp { get; set; } = default!;

    [Parameter] public ConsolidationTrackingView? consolidated { get; set; }
    [Parameter] public bool foundConsolidated { get; set; }
    [Parameter] public bool firstSearch { get; set; }

    private bool loading = false;

    private async Task GetConsolidatedTrackingAsync()
    {
        loading = true;
        await ConsolidationTrackingHttp.GetConsolidatedTrackingDownloadAsync(consolidated!.ConsolidatedId, true);
        loading = false;
    }

    private async Task GetConsolidatedPackagesAsync()
    {
        loading = true;
        await ConsolidationTrackingHttp.GetConsolidatedPackagesDownloadAsync(consolidated!.ConsolidatedId, true);
        loading = false;
    }
}