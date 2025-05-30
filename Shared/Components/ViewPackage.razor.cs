using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components;
public partial class ViewPackage
{
    [Inject] DialogService DialogService { get; set; } = default!;

    private bool paqueteEncontrado = true;
    private bool loading = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.Delay(500);
            loading = false;
            StateHasChanged();
        }
    }

    private Package model = new()
    {
        NumeroRastreo = "UKSIS-UKSIS-386759",
        UbicacionOrigen = "Oficinas Z 12",
        Origen = "Contabilidad",
        UbicacionDestino = "Unipark",
        Destino = "Sistemas",
        EmailDestinatario = "test@gmail.com",
        NombreDestinatario = "test",
        Observaciones = "Observaciones de prueba \ntexto de prueba",
        Detalles =
            [
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
            ]
    };

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