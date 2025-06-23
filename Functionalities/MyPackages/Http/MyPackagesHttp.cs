using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.MyPackages.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Services.Security;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.MyPackages.Http;
public class MyPackagesHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;
    private readonly GetCurrentUser _getCurrentUser;

    public MyPackagesHttp(ApiGetService apiGetService, 
                          CustomDialogService customDialogService, 
                          DialogService dialogService,
                          GetCurrentUser getCurrentUser)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
        _getCurrentUser = getCurrentUser;
    }

    public async Task<IList<MyPackagesView>> GetMyPackagesAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync($"paquetes", 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<MyPackagesView>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<MyPackagesView>();

            }
            else
            {
                await _customDialogService.OpenViewErrors(response);
            }
        } catch (Exception e)
        {
            await _dialogService.Alert(e.Message, "Error interno", new AlertOptions() { OkButtonText = "Aceptar" });
        }

        return new List<MyPackagesView>();
    }
}