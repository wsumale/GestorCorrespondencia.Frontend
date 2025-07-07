using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.ReceptionPendingConsolidations.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.ReceptionPendingConsolidations.Http;
public class ReceptionPendingConsolidationsHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public ReceptionPendingConsolidationsHttp(ApiGetService apiGetService, CustomDialogService customDialogService, DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }

    public async Task<IList<PendingConsolidationForReceptionModel>> GetPendingConsolidationsForReceptionAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync("consolidados?TipoConsolidado=2&Recibido=false", 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<PendingConsolidationForReceptionModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<PendingConsolidationForReceptionModel>();

            }
            else
            {
                await _customDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch (Exception e)
        {
            await _dialogService.Alert(e.Message, "Error interno", new AlertOptions() { OkButtonText = "Aceptar" });
        }

        return new List<PendingConsolidationForReceptionModel>();
    }
}