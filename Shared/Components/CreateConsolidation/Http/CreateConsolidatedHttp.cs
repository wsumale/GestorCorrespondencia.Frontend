using GestorCorrespondencia.Frontend.Functionalities.PackageShipping.DTO;
using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.DTO;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Http;
public class CreateConsolidatedHttp
{
    private readonly ApiPostService _apiPostService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public CreateConsolidatedHttp(ApiPostService apiPostService,
                                  CustomDialogService customDialogService,
                                  DialogService dialogService)
    {
        _apiPostService = apiPostService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }

    public async Task<ConsolidatedResponseDTO> SendSenderConsolidationAsync(ConsolidatedSenderRequestDTO dto)
    {
        try
        {
            var response = await _apiPostService.PostAsync("consolidados", dto, 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<ConsolidatedResponseDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new ConsolidatedResponseDTO();
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

        return new ConsolidatedResponseDTO();
    }

    public async Task<ConsolidatedResponseDTO> SendCorrespondenceConsolidationAsync(ConsolidatedCorrespondenceRequestDTO dto)
    {
        try
        {

            var response = await _apiPostService.PostAsync("consolidados", dto, 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<ConsolidatedResponseDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new ConsolidatedResponseDTO();
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
        return new ConsolidatedResponseDTO();
    }

}