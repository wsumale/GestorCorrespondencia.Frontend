using GestorCorrespondencia.Frontend.Shared.Components.CreatePackageIncident.Model;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components.CreatePackageIncident.Components;
public partial class CreatePackageIncident
{
    [Inject] private DialogService DialogService { get; set; } = default!;
    [Parameter] public int PackageId { get; set; }

    private IncidentFormModel form = new();
    private Dictionary<int, string> incidentType = new Dictionary<int, string>();

    protected override void OnInitialized()
    {
        incidentType.Add(1, "Destino incorrecto");
        incidentType.Add(2, "Destinatario incorrecto");
        incidentType.Add(3, "Paquete no recibido");
        base.OnInitialized();
    }

    private void Cancel()
    {
        DialogService.Close();
    }
}