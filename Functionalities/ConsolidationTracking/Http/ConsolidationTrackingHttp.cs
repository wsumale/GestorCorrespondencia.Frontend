using GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.JSInterop;
using Radzen;
using System.Text.Json;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Http;
public class ConsolidationTrackingHttp
{
    private readonly ApiGetService _apiGetService;
    private readonly IJSRuntime _iJSRuntime;
    private readonly CustomDialogService _customDialogService;
    private readonly DialogService _dialogService;
    private readonly ApiPatchService _apiPatchService;
    private readonly NotificationService _notificationService;

    public ConsolidationTrackingHttp(ApiGetService apiGetService,
                                     IJSRuntime iJSRuntime,
                                     CustomDialogService customDialogService,
                                     DialogService dialogService,
                                     ApiPatchService apiPatchService,
                                     NotificationService notificationService)
    {
        _apiGetService = apiGetService;
        _iJSRuntime = iJSRuntime;
        _customDialogService = customDialogService;
        _dialogService = dialogService;
        _apiPatchService = apiPatchService;
        _notificationService = notificationService;
    }

    public async Task<ConsolidationTrackingView?> GetConsolidationDetailAsync(int ConsolidatedId, bool CurrentUser)
    {
        try
        {
            var response = await _apiGetService.GetAsync($"consolidados/{ConsolidatedId}?UsuarioActual={CurrentUser}", "", 1, false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<ConsolidationTrackingView>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data;
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

        return null;
    }

    public async Task GetConsolidatedTrackingDownloadAsync(int ConsolidatedId, bool CurrentUser)
    {
        try
        {
            var response = await _apiGetService.GetAsync($"consolidados/{ConsolidatedId}?UsuarioActual={CurrentUser}", "application/pdf", 1, false);

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
    public async Task GetConsolidatedPackagesDownloadAsync(int ConsolidatedId, bool CurrentUser)
    {
        try
        {
            var response = await _apiGetService.GetAsync($"consolidados/{ConsolidatedId}/rastreos?UsuarioActual={CurrentUser}", "application/pdf", 1, false);

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