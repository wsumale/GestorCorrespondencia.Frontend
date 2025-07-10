using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components.ViewConsolidationDetail.Model;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components.ViewConsolidationDetail.Http;
public class ViewConsolidationDetailHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;
    private readonly ApiPatchService _apiPatchService;
    private readonly NotificationService _notificationService;

    public ViewConsolidationDetailHttp(ApiGetService apiGetService,
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

    public async Task<Consolidation?> GetConsolidationDetailAsync(int consolidatedId, bool CurrentUser)
    {
        try
        {
            var response = await _apiGetService.GetAsync($"consolidados/{consolidatedId}?UsuarioActual={CurrentUser}", "", 1, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Consolidation>(json, new JsonSerializerOptions
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