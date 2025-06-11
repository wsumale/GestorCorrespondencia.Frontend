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

    private bool loading = false;

    private PendingConsolidation? consolidated = new();

    protected override async void OnInitialized()
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

    private async Task CreateIncidentDialogAsync(int PackageId)
    {
        await CustomDialogService.OpenCreatePackageIncidentAsync(PackageId);
    }

    private async Task LoadDataAsync()
    {
        consolidated = await ConsolidationReceiveListHttp.GetPendingConsolidationDetailAsync(ConsolidatedId);

    }

    private void Cancel()
    {
        DialogService.Close();
    }
}