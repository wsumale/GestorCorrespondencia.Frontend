using GestorCorrespondencia.Frontend.Functionalities.AllConsolidations.Http;
using GestorCorrespondencia.Frontend.Functionalities.AllConsolidations.Model;
using GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Http;
using GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Pages;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.AllConsolidations.Pages;
public partial class AllConsolidations
{
    [Inject] AllConsolidationsHttp AllConsolidationsHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    private bool loading = false;

    private IList<AllConsolidationsView> consolidations = new List<AllConsolidationsView>();

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
        consolidations = await AllConsolidationsHttp.GetAllConsolidationsAsync();
    }
}