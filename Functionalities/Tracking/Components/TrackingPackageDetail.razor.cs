using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using Microsoft.AspNetCore.Components;

namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Components;
public partial class TrackingPackageDetail
{
    [Parameter] public Package? package { get; set; }

    private List<(string Texto, string Icon)> timelineItems = new()
    {
        ("Enviado a Correspondencia", "local_shipping"),
        ("Recibido por Correspondencia", "mail"),
        ("Enviado a Destino", "send"),
        ("En Recepción", "location_on"),
        ("Recibido", "check_circle")
    };

}