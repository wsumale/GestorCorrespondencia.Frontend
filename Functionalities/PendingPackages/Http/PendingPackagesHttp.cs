using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.PendingPackages.DTO;
using GestorCorrespondencia.Frontend.Functionalities.PendingPackages.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Services.Security;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Model;
using GestorCorrespondencia.Frontend.Shared.Interfaces;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.PendingPackages.Http;
public class PendingPackagesHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly ApiPatchService _apiPatchService;
    private readonly ApiPostService _apiPostService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;
    private readonly NotificationService _notificationService;
    private readonly GetCurrentUser _getCurrentUser;

    public PendingPackagesHttp(ApiGetService apiGetService,
                               ApiPatchService apiPatchService,
                               ApiPostService apiPostService,
                               CustomDialogService customDialogService,
                               DialogService dialogService,
                               NotificationService notificationService,
                               GetCurrentUser getCurrentUser)
    {
        _apiGetService = apiGetService;
        _apiPatchService = apiPatchService;
        _apiPostService = apiPostService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
        _notificationService = notificationService;
        _getCurrentUser = getCurrentUser;
    }

    public async Task<IList<PendingPackage>> GetPendingPackagesAsync()
    {
        try
        {
            var user = await _getCurrentUser.GetUserInfoAsync();
            var response = await _apiGetService.GetAsync($"paquetes?Estado=10&IdDestinatario={user.UserId}", "", 1, false);
            //var response = await _apiGetService.GetAsync("destinatarios/paquetes?Estado=10", 1, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<PendingPackage>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<PendingPackage>();

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
        return new List<PendingPackage>();
    }

    public async Task ReceiveAsync(int PackageId)
    {
        try
        {
            var response = await _apiPatchService.PatchWithoutBodyRequestAsync($"paquetes/{PackageId}", 1, true);
            if (response.IsSuccessStatusCode)
            {
                _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Operación exitosa", Detail = "Se recibió el paquete", Duration = 4000 });
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

    public async Task ChangeReceiverAsync(ChangeReceiverInDestinationDTO dto)
    {
        try
        {
            var response = await _apiPostService.PostAsync("incidencias", dto, 1, true);
            if (response.IsSuccessStatusCode)
            {
                _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Operación exitosa", Detail = "Destinatario actualizado", Duration = 4000 });
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