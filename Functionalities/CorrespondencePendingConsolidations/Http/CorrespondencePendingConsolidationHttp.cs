using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.CorrespondencePendingConsolidations.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.CorrespondencePendingConsolidations.Http;
public class CorrespondencePendingConsolidationHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public CorrespondencePendingConsolidationHttp(ApiGetService apiGetService, CustomDialogService customDialogService, DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }

    public async Task<IList<PendingConsolidationForCorrespondenceModel>> GetPendingConsolidationsForCorrespondenceAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync("consolidados?TipoConsolidado=1&Recibido=false", "", 1, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<PendingConsolidationForCorrespondenceModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<PendingConsolidationForCorrespondenceModel>();

            }
            else
            {
                await _customDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch(Exception e)
        {
            await _dialogService.Alert(e.Message, "Error interno", new AlertOptions() { OkButtonText = "Aceptar" });
        }

        return new List<PendingConsolidationForCorrespondenceModel>();
    }
}