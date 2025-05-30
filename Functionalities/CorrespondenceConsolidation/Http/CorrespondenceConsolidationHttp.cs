using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.CorrespondenceConsolidation.Http;
public class CorrespondenceConsolidationHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;

    public CorrespondenceConsolidationHttp(ApiGetService apiGetService, CustomDialogService customDialogService, DialogService dialogService)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
    }
}