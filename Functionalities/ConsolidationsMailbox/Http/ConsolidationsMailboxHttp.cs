using GestorCorrespondencia.Frontend.Functionalities.AllConsolidations.Model;
using GestorCorrespondencia.Frontend.Functionalities.ConsolidationsMailbox.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;
using System.Text.Json;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationsMailbox.Http;
public class ConsolidationsMailboxHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public ConsolidationsMailboxHttp(ApiGetService apiGetService,
                                     CustomDialogService customDialogService,
                                     DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }

    public async Task<IList<ConsolidationsMailboxView>> GetConsolidationsMailboxAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync("consolidados?TipoConsolidado=2", 1, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<ConsolidationsMailboxView>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<ConsolidationsMailboxView>();

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

        return new List<ConsolidationsMailboxView>();
    }

}