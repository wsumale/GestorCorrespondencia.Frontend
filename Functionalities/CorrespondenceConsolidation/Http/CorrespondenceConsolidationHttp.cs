using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Model;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.CorrespondenceConsolidation.Http;
public class CorrespondenceConsolidationHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public CorrespondenceConsolidationHttp(ApiGetService apiGetService, CustomDialogService customDialogService, DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }

    public async Task<IList<ConsolidatedDetail>> GetPackagesPendingConsolidationByLocationIdAsync(int LocationId)
    {
        try
        {
            var response = await _apiGetService.GetAsync($"paquetes?Estado=6&IdUbicacionDestinatario={LocationId}&IncluirPaquetesDescendientesDestino=true", 1, false);

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
                await _customDialogService.OpenViewErrors(response);
            }
        }
        catch (Exception e)
        {
            await _dialogService.Alert(e.Message, "Error interno", new AlertOptions() { OkButtonText = "Aceptar" });
        }

        return new List<ConsolidatedDetail>();
    }
}