using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Shared.Components.ConsolidationReceiveList.Http;
using GestorCorrespondencia.Frontend.Shared.Components.ConsolidationReceiveList.Model;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components.ConsolidationReceiveList.Components;
public partial class ConsolidationReceiveList
{
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] DialogService DialogService { get; set; } = default!;
    [Inject] ConsolidationReceiveListHttp ConsolidationReceiveListHttp { get; set; } = default!;

    [Parameter] public int ConsolidatedId { get; set; }
    [Parameter] public int ConsolidatedType { get; set; }

    private bool loading = false;
    private bool busy = false;

    private PendingConsolidation? consolidated = new();

    protected override async Task OnInitializedAsync()
    {
        loading = true;

        await LoadDataAsync();
        base.OnInitialized();

        loading = false;
        StateHasChanged();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        StateHasChanged();
        base.OnAfterRender(firstRender);
    }

    private async Task CreateIncidentDialogAsync(int PackageId, int ConsolidatedDetailId)
    {
        await CustomDialogService.OpenCreatePackageIncidentAsync(PackageId, ConsolidatedDetailId);
        await RefreshLoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        consolidated = await ConsolidationReceiveListHttp.GetPendingConsolidationDetailAsync(ConsolidatedId);
    }

    private async Task RefreshLoadDataAsync()
    {
        loading = true;
        await LoadDataAsync();
        loading = false;
        StateHasChanged();
    }

    private async Task ReceiveConsolidationAsync()
    {
        busy = loading = true;
        await ConsolidationReceiveListHttp.ReceiveConsolidatedPackagesAsync(ConsolidatedId);
        busy = loading = false;
    }

    private void Cancel()
    {
        DialogService.Close();
    }
}