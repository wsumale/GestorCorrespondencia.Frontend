using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Model;
using Radzen;

namespace GestorCorrespondencia.Frontend.Services.SGL;
public class SGLService
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public SGLService(ApiGetService apiGetService, CustomDialogService customDialogService, DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }

    public async Task<IList<Location>> GetLocationsSendPackagesAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync("ubicaciones?Correspondencia=true", 4, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<Location>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<Location>();
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

        return new List<Location>();
    }

    public async Task<IList<Location>> GetLocationsSendConsolidatesAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync("ubicaciones?TipoListadoUbicaciones=2", 4, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<Location>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<Location>();
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

        return new List<Location>();
    }

}