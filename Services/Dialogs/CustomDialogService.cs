using GestorCorrespondencia.Frontend.Shared.Components;
using Radzen;
using GestorCorrespondencia.Frontend.Shared.Model;
using System.Text.Json;
using GestorCorrespondencia.Frontend.Shared.Components.PreviewNewConsolidation.Model;
using GestorCorrespondencia.Frontend.Shared.Components.PreviewNewConsolidation.Components;

namespace GestorCorrespondencia.Frontend.Services.Dialogs;
public class CustomDialogService
{
    private readonly DialogService DialogService;

    public CustomDialogService(DialogService dialogService)
    {
        DialogService = dialogService;
    }

    public async Task OpenViewPackage ()
    {
        await DialogService.OpenAsync<ViewPackage>("Detalle del Paquete", null, new DialogOptions
        {
            Width = "75%",
            Height = "60%",
            Style = "min-height: auto; min-width: auto;"
        });
    }

    public async Task OpenPreviewNewConsolidation (Consolidated consolidated)
    {
        
        await DialogService.OpenAsync<PreviewNewConsolidation>(
            "Detalle consolidado",
            new Dictionary<string, object>
            {
                { "consolidated", consolidated }
            },
            new DialogOptions { CssClass = "consolidation-dialog", Width = "75%" });
    }

    public async Task OpenViewErrors (HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
            await DialogService.Alert(response.ReasonPhrase, $"Error código ({(int)response.StatusCode})", new AlertOptions() { OkButtonText = "Aceptar" });
        } 
        else
        {
            var errorObject = JsonSerializer.Deserialize<ApiResponseNotAcceptable>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var statusDialog = await DialogService.OpenAsync<ViewErrorsModal>(
                errorObject!.Title,
                new Dictionary<string, object>
                    { { "responseNotAcceptable", errorObject } },
                new DialogOptions
                    { Width = "400px", CloseDialogOnEsc = false, CloseDialogOnOverlayClick = false, ShowClose = false }
            );
        }
    }
}