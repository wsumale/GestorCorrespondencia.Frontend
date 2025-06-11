using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components.PreviewNewConsolidation.DTO;
using GestorCorrespondencia.Frontend.Shared.Components.PreviewNewConsolidation.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components.PreviewNewConsolidation.Components
{
    public partial class PreviewNewConsolidation
    {
        [Inject] DialogService DialogService { get; set; } = default!;
        [Inject] ApiPostService ApiPostService { get; set; } = default!;
        [Inject] CustomDialogService CustomDialogService { get; set; } = default!;
        [Inject] NavigationManager NavigationManager { get; set; } = default!;
        [Inject] IJSRuntime JS { get; set; } = default!;
        [Inject] ILogger<PreviewNewConsolidation> _logger { get; set; } = default!;

        [Parameter] public Consolidated? consolidated { get; set; }

        bool loading = false;

        public async Task Submit()
        {
            try
            {
                loading = true;

                List<int> packageIds = consolidated!.ConsolidatedDetail.Select(d => d.PackageId).ToList();

                object? dto = null;
                if (consolidated!.Type == 1)
                {
                    dto = new ConsolidatedSenderRequestDTO { ConsolidatedType = consolidated.Type, PackagesIds = packageIds };
                }
                else if (consolidated!.Type == 2)
                {
                    dto = new ConsolidatedCorrespondenceRequestDTO { ConsolidatedType = consolidated.Type, RecipientLocationId = consolidated.RecipientLocationId, PackagesIds = packageIds };
                }

                var response = await ApiPostService.PostAsync("consolidados", dto, 1, true);

                if (response.IsSuccessStatusCode)
                {
                    var fileBytes = await response.Content.ReadAsByteArrayAsync();
                    var contentType = response.Content.Headers.ContentType?.ToString() ?? "application/octet-stream";
                    var fileName = response.Content.Headers.ContentDisposition?.FileName?.Trim('"') ?? "archivo.xlsx";

                    var base64 = Convert.ToBase64String(fileBytes);

                    await JS.InvokeVoidAsync("downloadFromByteArray", fileName, base64, contentType);

                    await Success();
                }
                else
                {
                    await CustomDialogService.OpenViewErrors(response);
                }
            } catch(Exception e)
            {
                await DialogService.Alert(e.Message, "Error interno", new AlertOptions { OkButtonText = "Aceptar" });
            } finally
            {
                loading = false;
            }
        }

        private async Task Success()
        {

            var redirect = await DialogService.Alert("Consolidado creado con éxito", "Operación exitosa", new AlertOptions { CloseDialogOnEsc = false, CloseDialogOnOverlayClick = false, OkButtonText = "Aceptar" });
            if (redirect == true)
            {
                NavigationManager.NavigateTo("/principal/home");
            }
        }

        public void Cancel()
        {
            DialogService.Close();
        }
    }
}