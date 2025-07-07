using GestorCorrespondencia.Frontend.Functionalities.Tracking.Http;
using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Pages;
public partial class Tracking
{
    [Inject] TrackingHttp TrackingHttp { get; set; } = default!;

    private bool loading = false;

    private int? PackageId;
    private Package package = new();
    private bool foundPackage;
    private bool firstSearch = false;

    private async Task SearchPackageAsync()
    {
        loading = true;
        package  = await TrackingHttp.GetPackageAsync(PackageId ?? 0, true);
        foundPackage = package != null && package.PackageId > 0;
        firstSearch = true;
        loading = false;
        StateHasChanged();
    }

    IList<PackageChangelog> changelog = new List<PackageChangelog>
    {
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "Recibido", Usuario = "scollections", Fecha = "05/04/2025" },
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "En Recepción", Usuario = "scollections", Fecha = "04/04/2025" },
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "Enviado a Destino", Usuario = "scollections", Fecha = "03/04/2025" },
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "Recibido por Correspondencia", Usuario = "scollections", Fecha = "02/04/2025" },
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "Enviado a Correspondencia", Usuario = "scollections", Fecha = "01/04/2025" },
        new PackageChangelog { NumeroRastreo = "UKSIS-UKSIS-386759", Comentario = "Cambio de estado", Estado = "Nuevo Envío", Usuario = "scollections", Fecha = "31/03/2025" }
    };

    private List<(string Texto, string Icon)> timelineItems = new()
    {
        ("Enviado a Correspondencia", "local_shipping"),
        ("Recibido por Correspondencia", "mail"),
        ("Enviado a Destino", "send"),
        ("En Recepción", "location_on"),
        ("Recibido", "check_circle")
    };

}