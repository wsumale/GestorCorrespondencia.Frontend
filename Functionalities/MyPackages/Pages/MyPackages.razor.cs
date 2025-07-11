using GestorCorrespondencia.Frontend.Functionalities.MyPackages.Http;
using GestorCorrespondencia.Frontend.Functionalities.MyPackages.Model;
using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.MyPackages.Pages;
public partial class MyPackages
{
    [Inject] MyPackagesHttp MyPackagesHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    bool loading = false;
    private IList<MyPackagesView> myPackages = new List<MyPackagesView>();

    protected override async Task OnInitializedAsync()
    {
        loading = true;
        myPackages = await MyPackagesHttp.GetMyPackagesAsync();
        loading = false;

        StateHasChanged();
    }

}