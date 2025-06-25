using GestorCorrespondencia.Frontend.Shared.Components;
using Radzen;
using GestorCorrespondencia.Frontend.Shared.Model;
using System.Text.Json;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Model;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Components;
using GestorCorrespondencia.Frontend.Shared.Components.ViewPackageDetail.Components;
using GestorCorrespondencia.Frontend.Shared.Components.CreatePackageIncident.Components;
using GestorCorrespondencia.Frontend.Shared.Components.ConsolidationReceiveList.Components;
using GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;

namespace GestorCorrespondencia.Frontend.Services.Dialogs;
public class CustomDialogService
{
    private readonly DialogService DialogService;

    public CustomDialogService(DialogService dialogService)
    {
        DialogService = dialogService;
    }

    public async Task OpenViewPackage (int PackageId)
    {
        await DialogService.OpenAsync<ViewPackageDetail>("Detalle del Paquete", 
            new Dictionary<string, object>
            {
                { "PackageId", PackageId }
            }, 
            new DialogOptions
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
            new DialogOptions { CssClass = "consolidation-dialog", Width = "75%" }
        );
    }

    public async Task OpenConsolidatedReceptionListAsync(int ConsolidatedId)
    {
        await DialogService.OpenAsync<ConsolidationReceiveList>(
            $"Consolidado: <strong>{ConsolidatedId}</strong>",
            new Dictionary<string, object?>
            {
                { "ConsolidatedId", ConsolidatedId }
            },
            new DialogOptions { Width = "75%" }
        );
    }

    public async Task OpenCreatePackageIncidentAsync(int PackageId, int ConsolidatedDetailId)
    {
        await DialogService.OpenAsync<CreatePackageIncident>(
            $"Crear incidencia, paquete: <strong>{PackageId}</strong>",
            new Dictionary<string, object?>
            {
                { "PackageId", PackageId },
                { "ConsolidatedDetailId", ConsolidatedDetailId }
            },
            new DialogOptions { Width = "500px" }
        );
    }

    public async Task<bool> OpenConfirm (string Message, string Title, string ConfirmText, string CancelText, DialogOptions Options)
    {
        bool response = await DialogService.OpenAsync<Confirmation>(
            Title,
            new Dictionary<string, object?>
            {
                { "message", Message },
                { "ConfirmText", ConfirmText },
                { "CancelText", CancelText }
            },
            new DialogOptions
            {
                CloseDialogOnEsc = false,
                ShowClose = false,
                Width = Options.Width
            }
        );

        return response;
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

    public async Task OpenInternalError(Exception e)
    {
        await DialogService.Alert(e.Message, "Error interno", new AlertOptions { OkButtonText = "Aceptar" });
    }

}