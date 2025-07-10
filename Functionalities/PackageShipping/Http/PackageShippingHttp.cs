using GestorCorrespondencia.Frontend.Functionalities.MyPackages.Model;
using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;
using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.DTO;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Http;
public class PackageShippingHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly ApiPatchService _apiPatchService;
    private readonly ApiPostService _apiPostService;
    private readonly CustomDialogService _customDialogService;
    private readonly NotificationService _notificationService;
    private readonly DialogService _dialogService;

    public PackageShippingHttp(ApiGetService apiGetService,
                               ApiPatchService apiPatchService,
                               ApiPostService apiPostService,
                               CustomDialogService customDialogService,
                               NotificationService notificationService,
                               DialogService dialogService)
    {

        _apiGetService = apiGetService;
        _apiPatchService = apiPatchService;
        _apiPostService = apiPostService;
        _customDialogService = customDialogService;
        _notificationService = notificationService;
        _dialogService = dialogService;
    }

    public async Task<IList<PackageDetailTypeItem>> GetPackageDetailTypeItemsAsync()
    {
        try
        {
            var response = await _apiGetService.GetAsync("paquetes/detalles/items", "", 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<PackageDetailTypeItem>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<PackageDetailTypeItem>();

            }
            else
            {
                await _customDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch (Exception e)
        {
            await _customDialogService.OpenInternalErrorAsync(e);
        }

        return new List<PackageDetailTypeItem>();
    }

    public async Task<PackageResponseDTO> SendPackagedAsync(PackageRequestDTO dto)
    {
        try
        {
            var response = await _apiPostService.PostAsync("paquetes", dto, 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<PackageResponseDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new PackageResponseDTO();
            }
            else
            {
                await _customDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch (Exception e)
        {
            await _customDialogService.OpenInternalErrorAsync(e);
        }
        return new PackageResponseDTO();
    }
}