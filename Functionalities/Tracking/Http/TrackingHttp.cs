using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Model;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Http;
public class TrackingHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public TrackingHttp(ApiGetService apiGetService, CustomDialogService customDialogService, DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }

    public async Task<Package> GetPackageAsync(int PackageId, bool CurrentUser)
    {
        try
        {
            var response = await _apiGetService.GetAsync($"paquetes/{PackageId}?UsuarioActual={CurrentUser}", 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<Package>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new Package();

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

        return new Package();
    }
}