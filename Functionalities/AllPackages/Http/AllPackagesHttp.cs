using GestorCorrespondencia.Frontend.Functionalities.AllPackages.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Microsoft.JSInterop;
using Radzen;
using System.Text.Json;

namespace GestorCorrespondencia.Frontend.Functionalities.AllPackages.Http;
public class AllPackagesHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;
    private readonly IJSRuntime _iJSRuntime;

    public AllPackagesHttp(ApiGetService apiGetService,
                           CustomDialogService customDialogService,
                           DialogService dialogService,
                           IJSRuntime iJSRuntime)
    {
        _apiGetService = apiGetService;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
        _iJSRuntime = iJSRuntime;
    }

    public async Task<IList<AllPackagesView>> GetAllPackagesAsync(AllPackagesFilterForm filterForm)
    {
        try
        {
            List<string> queryParams = new List<string>();

            if (filterForm.SenderLocationId > 0)
                queryParams.Add($"IdUbicacionRemitente={filterForm.SenderLocationId}");

            if (filterForm.SenderId > 0)
                queryParams.Add($"IdRemitente={filterForm.SenderId}");

            if (filterForm.ReceiverLocationId > 0)
                queryParams.Add($"IdUbicacionDestinatario={filterForm.ReceiverLocationId}");

            if (filterForm.ReceiverId > 0)
                queryParams.Add($"IdDestinatario={filterForm.ReceiverId}");

            if (filterForm.State > 0)
                queryParams.Add($"Estado={filterForm.State}");

            string queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            string endpoint = $"paquetes{queryString}";

            var response = await _apiGetService.GetAsync(endpoint, "", 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IList<AllPackagesView>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data ?? new List<AllPackagesView>();

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

        return new List<AllPackagesView>();
    }

    public async Task GetAllPackagesDownloadAsync(AllPackagesFilterForm filterForm, string Accept)
    {
        try
        {
            List<string> queryParams = new List<string>();

            if (filterForm.SenderLocationId > 0)
                queryParams.Add($"IdUbicacionRemitente={filterForm.SenderLocationId}");

            if (filterForm.SenderId > 0)
                queryParams.Add($"IdRemitente={filterForm.SenderId}");

            if (filterForm.ReceiverLocationId > 0)
                queryParams.Add($"IdUbicacionDestinatario={filterForm.ReceiverLocationId}");

            if (filterForm.ReceiverId > 0)
                queryParams.Add($"IdDestinatario={filterForm.ReceiverId}");

            if (filterForm.State > 0)
                queryParams.Add($"Estado={filterForm.State}");

            string queryString = queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "";
            string endpoint = $"paquetes{queryString}";

            var response = await _apiGetService.GetAsync(endpoint, Accept, 1, false);

            if (response.IsSuccessStatusCode)
            {

                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";
                var fileName = response.Content.Headers.ContentDisposition?.FileName?.Trim('"') ?? "archivo.xlsx";

                var base64 = Convert.ToBase64String(fileBytes);

                await _iJSRuntime.InvokeVoidAsync("downloadFromByteArray", fileName, base64, contentType);
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
    }
}