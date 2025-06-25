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

    private bool paqueteEncontrado = true;
    private bool loading = true;
    private Package package = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            package = await TrackingHttp.GetPackageAsync(PackageId);
            loading = false;
            StateHasChanged();
        }
    }

    IList<PackageChangelog> changelog =
    [
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "Recibido", Usuario = "scollections", Fecha = "05/04/2025" },
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "En Recepción", Usuario = "scollections", Fecha = "04/04/2025" },
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "Enviado a Destino", Usuario = "scollections", Fecha = "03/04/2025" },
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "Recibido por Correspondencia", Usuario = "scollections", Fecha = "02/04/2025" },
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "Enviado a Correspondencia", Usuario = "scollections", Fecha = "01/04/2025" },
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "Nuevo Envío", Usuario = "scollections", Fecha = "31/03/2025" }
    ];
}