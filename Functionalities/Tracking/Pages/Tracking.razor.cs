using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Pages;
public partial class Tracking
{
    [Inject] DialogService DialogService { get; set; } = default!;

    private bool paqueteEncontrado = true;

    private Package model = new Package
    {
        NumeroRastreo = "UKSIS-UKSIS-386759",
        UbicacionOrigen = "Oficinas Z 12",
        Origen = "Contabilidad",
        UbicacionDestino = "Unipark",
        Destino = "Sistemas",
        EmailDestinatario = "test@gmail.com",
        NombreDestinatario = "test",
        Observaciones = "test test test test test test test test \ntest test test test test test test test test test test test test test test test test test test test test test test test",
        Detalles = new List<PackageDetail>
        {
            new PackageDetail { Type = "Documentos", Comment = "Planillas ", Quantity = 2 },
            new PackageDetail { Type = "Paquetes", Comment = "Camisas", Quantity = 4 },
            new PackageDetail { Type = "Documentos", Comment = "Planillas ", Quantity = 2 },
            new PackageDetail { Type = "Caja", Comment = "Etiquetas", Quantity = 2 },
            new PackageDetail { Type = "Paquetes", Comment = "Camisas", Quantity = 4 },
            new PackageDetail { Type = "Documentos", Comment = "Planillas ", Quantity = 2 },
            new PackageDetail { Type = "Caja", Comment = "Etiquetas", Quantity = 2 },
            new PackageDetail { Type = "Paquetes", Comment = "Camisas", Quantity = 4 },
            new PackageDetail { Type = "Documentos", Comment = "Planillas ", Quantity = 2 },
            new PackageDetail { Type = "Caja", Comment = "Etiquetas", Quantity = 2 },
            new PackageDetail { Type = "Paquetes", Comment = "Camisas", Quantity = 4 },
            new PackageDetail { Type = "Documentos", Comment = "Planillas ", Quantity = 2 },
            new PackageDetail { Type = "Caja", Comment = "Etiquetas", Quantity = 2 }
        }
    };

    private void BuscarPaquete()
    {
        paqueteEncontrado = true;
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


    private Orientation orientation = Orientation.Horizontal;

    private List<(string Texto, string Icon)> timelineItems = new()
    {
        ("Enviado a Correspondencia", "local_shipping"),
        ("Recibido por Correspondencia", "mail"),
        ("Enviado a Destino", "send"),
        ("En Recepción", "location_on"),
        ("Recibido", "check_circle")
    };

}