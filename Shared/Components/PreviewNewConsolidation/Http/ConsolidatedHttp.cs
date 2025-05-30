using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components.PreviewNewConsolidation.DTO;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components.PreviewNewConsolidation.Http;
public class ConsolidatedHttp
{
    private readonly ApiPostService _apiPostService;
    private readonly CustomDialogService _customDialogService;

    public ConsolidatedHttp(ApiPostService apiPostService,
                            CustomDialogService customDialogService)
    {
        _apiPostService = apiPostService;
        _customDialogService = customDialogService;
    }

    public async Task CreateConsolidatedAsync(ConsolidatedCorrespondenceRequestDTO dto)
    {
        var response = await _apiPostService.PostAsync("consolidados", dto, 1, true);

        if (response.IsSuccessStatusCode)
        {

        }
        else
        {
            await _customDialogService.OpenViewErrors(response);
        }
    }
}