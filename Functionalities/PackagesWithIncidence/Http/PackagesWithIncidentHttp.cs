using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.DTO;
using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Model;
using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Http;
public class PackagesWithIncidentHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly ApiPatchService _apiPatchService;
    private readonly CustomDialogService _customDialogService;
    private readonly NotificationService _notificationService;
    private readonly DialogService _dialogService;

    public PackagesWithIncidentHttp(ApiGetService apiGetService,
                                    ApiPatchService apiPatchService,
                                    CustomDialogService customDialogService,
                                    NotificationService notificationService,
                                    DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _apiPatchService = apiPatchService;
        _customDialogService = customDialogService;
        _notificationService = notificationService;
        _dialogService = dialogService;
    }

    public async Task<IList<PackageWithIncident>> GetPackagesWithIncidentAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync("incidencias", 1, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<PackageWithIncident>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<PackageWithIncident>();

            }
            else
            {
                await _customDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch (Exception e)
        {
            await _customDialogService.OpenInternalErrorAsync(e);
        }
        return new List<PackageWithIncident>();
    }

    public async Task<Incident> GetIncidentDetailAsync(int IncidentId)
    {
        try
        {
            var response = await _apiGetService.GetAsync($"incidencias/{IncidentId}", 1, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Incident>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new Incident();
            }
            else
            {
                await _customDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch (Exception e)
        {
            await _customDialogService.OpenInternalErrorAsync(e);
        }
        return new Incident();
    } 

    public async Task ResolveChangeOfRecipientIncidentAsync(int IncidentId, IncidentResolveDTO DTO, string SuccessMessage)
    {
        try
        {
            var response = await _apiPatchService.PatchAsync($"incidencias/{IncidentId}", DTO, 1, true);
            if (response.IsSuccessStatusCode)
            {
                _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Operación exitosa", Detail = SuccessMessage, Duration = 4000 });
                _dialogService.Close();
            }
            else
            {
                await _customDialogService.OpenViewErrorsAsync(response);
                _dialogService.Close();
            }
        }
        catch (Exception e)
        {
            await _customDialogService.OpenInternalErrorAsync(e);
        }
    }

    public async Task ResolveReturnToSenderIncidentAsync(int IncidentId, ReturnToSenderDTO DTO, string SuccessMessage)
    {
        try
        {
            var response = await _apiPatchService.PatchAsync($"incidencias/{IncidentId}", DTO, 1, true);
            if (response.IsSuccessStatusCode)
            {
                _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Operación exitosa", Detail = SuccessMessage, Duration = 4000 });
                _dialogService.Close();
            }
            else
            {
                await _customDialogService.OpenViewErrorsAsync(response);
                _dialogService.Close();
            }
        }
        catch (Exception e)
        {
            await _customDialogService.OpenInternalErrorAsync(e);
        }
    }
}