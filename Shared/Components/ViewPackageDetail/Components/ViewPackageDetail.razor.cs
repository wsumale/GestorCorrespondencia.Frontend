using GestorCorrespondencia.Frontend.Functionalities.Tracking.Http;
using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components.ViewPackageDetail.Components;
public partial class ViewPackageDetail
{
    [Inject] DialogService DialogService { get; set; } = default!;
    [Inject] TrackingHttp TrackingHttp { get; set; } = default!;

    [Parameter] public int PackageId { get; set; }

    private bool paqueteEncontrado = false;
    private bool loading = true;
    private Package package = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            package = await TrackingHttp.GetPackageAsync(PackageId, false);
            loading = false;
            paqueteEncontrado = true;
            StateHasChanged();
        }
    }
}