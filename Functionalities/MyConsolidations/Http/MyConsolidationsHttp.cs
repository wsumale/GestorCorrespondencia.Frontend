using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Model;
using GestorCorrespondencia.Frontend.Functionalities.MyPackages.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Services.Security;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.MyConsolidations.Http;
public class MyConsolidationsHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;
    private readonly GetCurrentUser _getCurrentUser;

    public MyConsolidationsHttp(ApiGetService apiGetService,
                                CustomDialogService customDialogService,
                                DialogService dialogService,
                                GetCurrentUser getCurrentUser)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
        _getCurrentUser = getCurrentUser;
    }

    public async Task<IList<MyConsolidationsView>> GetMyConsolidationsAsync()
    {
        try
        {
            var user = await _getCurrentUser.GetUserInfoAsync();

            var response = await _apiGetService.GetAsync($"consolidados?IdUsuarioCreador={user.UserId}", "", 1, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<MyConsolidationsView>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<MyConsolidationsView>();

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

        return new List<MyConsolidationsView>();
    }

}