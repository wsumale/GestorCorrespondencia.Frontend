using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components.ConsolidationReceiveList.Model;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components.ConsolidationReceiveList.Http;
public class ConsolidationReceiveListHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;
    private readonly ApiPatchService _apiPatchService;
    private readonly NotificationService _notificationService;

    public ConsolidationReceiveListHttp(ApiGetService apiGetService, 
                                        CustomDialogService customDialogService, 
                                        DialogService dialogService, 
                                        ApiPatchService apiPatchService,
                                        NotificationService notificationService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
        _apiPatchService = apiPatchService;
        _notificationService = notificationService;
    }

    public async Task<PendingConsolidation?> GetPendingConsolidationDetailAsync(int consolidatedId)
    {
        try
        {
            var response = await _apiGetService.GetAsync($"consolidados/{consolidatedId}", 1, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<PendingConsolidation>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data;
            }
            else
            {
                await _customDialogService.OpenViewErrors(response);
            }
        }
        catch (Exception e)
        {
            await _customDialogService.OpenInternalError(e);
        }

        return null;
    }

    public async Task ReceiveConsolidatedPackagesAsync(int ConsolidatedId)
    {
        try
        {
            var response = await _apiPatchService.PatchWithoutBodyRequestAsync($"consolidados/{ConsolidatedId}", 1, true);
            if (response.IsSuccessStatusCode)
            {
                _notificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Operación exitosa", Detail="Se recibió el consolidado", Duration = 4000 });
                _dialogService.Close();
            }
            else
            {
                await _customDialogService.OpenViewErrors(response);
                _dialogService.Close();
            }
        } 
        catch (Exception e)
        {
            await _customDialogService.OpenInternalError(e);
        }
    }
}