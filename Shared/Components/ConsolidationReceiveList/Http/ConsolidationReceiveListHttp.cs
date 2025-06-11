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

    public ConsolidationReceiveListHttp(ApiGetService apiGetService, CustomDialogService customDialogService, DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }

    public async Task<PendingConsolidation?> GetPendingConsolidationDetailAsync(int consolidatedId)
    {
        try
        {
            var response = await _apiGetService.GetAsync($"consolidados/{consolidatedId}", 1, false);

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
            await _dialogService.Alert(e.Message, "Error interno", new AlertOptions() { OkButtonText = "Aceptar" });
        }

        return null;
    }
}