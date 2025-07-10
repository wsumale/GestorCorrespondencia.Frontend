using GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;
using System.Text.Json;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Http;
public class ConsolidationTrackingHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;
    private readonly ApiPatchService _apiPatchService;
    private readonly NotificationService _notificationService;

    public ConsolidationTrackingHttp(ApiGetService apiGetService,
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

    public async Task<ConsolidationTrackingView?> GetConsolidationDetailAsync(int ConsolidatedId, bool CurrentUser)
    {
        try
        {
            var response = await _apiGetService.GetAsync($"consolidados/{ConsolidatedId}?UsuarioActual={CurrentUser}", "", 1, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<ConsolidationTrackingView>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data;
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

        return null;
    }
}