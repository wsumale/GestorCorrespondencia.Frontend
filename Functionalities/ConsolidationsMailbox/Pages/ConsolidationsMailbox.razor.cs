using GestorCorrespondencia.Frontend.Functionalities.ConsolidationsMailbox.Http;
using GestorCorrespondencia.Frontend.Functionalities.ConsolidationsMailbox.Model;
using GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Http;
using GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationsMailbox.Pages;
public partial class ConsolidationsMailbox
{
    [Inject] ConsolidationsMailboxHttp ConsolidationsMailboxHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    private bool loading = false;

    private IList<ConsolidationsMailboxView> consolidations = new List<ConsolidationsMailboxView>();

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
        consolidations = await ConsolidationsMailboxHttp.GetConsolidationsMailboxAsync();
    }
}