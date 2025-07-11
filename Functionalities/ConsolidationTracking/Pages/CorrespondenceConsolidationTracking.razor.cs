using GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Http;
using GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Pages;
public partial class CorrespondenceConsolidationTracking
{
    [Inject] ConsolidationTrackingHttp ConsolidationTrackingHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] IJSRuntime JS { get; set; } = default!;

    [Parameter] public int? ConsolidationIdParam { get; set; }

    private bool loading = false;
    private ConsolidationTrackingForm form = new();
    private ConsolidationTrackingView? consolidated = new();

    private bool foundConsolidated;
    private bool firstSearch = false;

    private string buffer = "";
    private string LastScanned = "";

    protected override async Task OnInitializedAsync()
    {
        if (ConsolidationIdParam > 0)
        {
            form.ConsolidationId = ConsolidationIdParam;
            await SearchConsolidationAsync();
        }
    }

    private async Task SearchConsolidationAsync()
    {
        loading = true;
        consolidated = await ConsolidationTrackingHttp.GetConsolidationDetailAsync(form.ConsolidationId ?? 0, false);
        foundConsolidated = consolidated != null && consolidated.ConsolidatedId > 0;
        firstSearch = true;
        loading = false;
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeVoidAsync("startGlobalScanner", DotNetObjectReference.Create(this));
        }
    }

    [JSInvokable]
    public async Task OnScannerInput(string code)
    {
        LastScanned = code;
        buffer = "";
        await ProcessScannedCodeAsync(code);
        StateHasChanged();
    }

    private async Task ProcessScannedCodeAsync(string code)
    {
        form.ConsolidationId = int.Parse(code);
        await SearchConsolidationAsync();
    }

}