using GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Http;
using GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Pages;
public partial class MyConsolidations
{
    [Inject] MyConsolidationsHttp MyConsolidationsHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    private bool loading = false;

    private IList<MyConsolidationsView> myConsolidations = new List<MyConsolidationsView>();

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
        myConsolidations = await MyConsolidationsHttp.GetMyConsolidationsAsync();
    }
}