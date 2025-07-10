using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Model;

namespace GestorCorrespondencia.Frontend.Functionalities.SenderConsolidation.Http;
public class SenderConsolidationHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public SenderConsolidationHttp(ApiGetService apiGetService, CustomDialogService customDialogService, DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }
    public async Task<IList<ConsolidatedDetail>> GetPackagesPendingConsolidationAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync("remitentes/paquetes?Estado=4", "", 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<ConsolidatedDetail>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<ConsolidatedDetail>();

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

        return new List<ConsolidatedDetail>();
    }
}