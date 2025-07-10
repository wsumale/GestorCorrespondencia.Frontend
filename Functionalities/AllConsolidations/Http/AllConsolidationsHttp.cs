using GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Model;
using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Services.Security;
using Radzen;
using GestorCorrespondencia.Frontend.Functionalities.AllConsolidations.Model;

namespace GestorCorrespondencia.Frontend.Functionalities.AllConsolidations.Http;
public class AllConsolidationsHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public AllConsolidationsHttp(ApiGetService apiGetService,
                                CustomDialogService customDialogService,
                                DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }

    public async Task<IList<AllConsolidationsView>> GetAllConsolidationsAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync("consolidados", "", 1, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<AllConsolidationsView>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<AllConsolidationsView>();

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

        return new List<AllConsolidationsView>();
    }

}