using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components.CreatePackageIncident.DTO;
using GestorCorrespondencia.Frontend.Shared.Components.CreatePackageIncident.Model;
using GestorCorrespondencia.Frontend.Shared.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components.CreatePackageIncident.Components;
public partial class CreatePackageIncident
{
    [Inject] private DialogService DialogService { get; set; } = default!;
    [Inject] private ApiPostService ApiPostService { get; set; } = default!;
    [Inject] private CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Parameter] public int PackageId { get; set; }
    [Parameter] public int ConsolidatedDetailId { get; set; }

    private bool loading = false;
    private bool busy = false;

    private IncidentFormModel form = new();

    private IEnumerable<object> incidentTypeValues => Enum.GetValues(typeof(IncidentType))
    .Cast<IncidentType>()
    .Select(e => new { Value = (int)e, Description = e.Description() });

    private async Task OnSubmitAsync()
    {
        try
        {
            loading = busy = true;

            IncidentDTO dto = new IncidentDTO();
            dto.ConsolidatedDetailId = ConsolidatedDetailId;
            dto.IncidentTypeId = form.IncidentType;
            dto.Comment = form.Comment;
            dto.RelationshipType = 2;

            var response = await ApiPostService.PostAsync("incidencias", dto, 1, true);

            if (response.IsSuccessStatusCode)
            {
                Success();
            }
            else
            {
                await CustomDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch(Exception e)
        {
            await CustomDialogService.OpenInternalErrorAsync(e);
        }
        finally
        {
            loading = busy = false;
        }
    }

    private void Success()
    {
        DialogService.Close();
        NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Operación exitosa", Detail = $"Se creó la incidencia para el paquete {PackageId}", Duration = 5000 });
    }

    private void Cancel()
    {
        DialogService.Close();
    }
}