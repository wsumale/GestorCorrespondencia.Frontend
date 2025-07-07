using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Services.Security;
using GestorCorrespondencia.Frontend.Shared.Model;
using Radzen;

namespace GestorCorrespondencia.Frontend.Services.SGU;
public class SGUService
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;
    private readonly GetCurrentUser _getCurrentUser;

    public SGUService(ApiGetService apiGetService, CustomDialogService customDialogService, DialogService dialogService, GetCurrentUser getCurrentUser)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
        _getCurrentUser = getCurrentUser;
    }

    public async Task<IList<User>> GetUsersByLocationAsync(int LocationId)
    {
        try
        {
            var user = await _getCurrentUser.GetUserInfoAsync();
            var response = await _apiGetService.GetAsync($"ubicaciones/{LocationId.ToString()}/usuarios?UserId={user.UserId}", 3, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<User>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<User>();
            }
        }
        catch (Exception e)
        {
            await _dialogService.Alert(e.Message, "Error interno", new AlertOptions() { OkButtonText = "Aceptar" });
        }

        return new List<User>();
    }

    public async Task<IList<User>> GetUsersPhysicalLocationFromUserLocation(int LocationId)
    {
        try
        {
            var user = await _getCurrentUser.GetUserInfoAsync();
            var response = await _apiGetService.GetAsync($"ubicaciones/{LocationId.ToString()}/usuarios?UserId={user.UserId}", 3, true);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<User>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<User>();
            }
        }
        catch (Exception e)
        {
            await _customDialogService.OpenInternalErrorAsync(e);
        }

        return new List<User>();
    }
}