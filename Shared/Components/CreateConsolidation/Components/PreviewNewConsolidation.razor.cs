using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.DTO;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Http;
using GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;

namespace GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.Components
{
    public partial class PreviewNewConsolidation
    {
        [Inject] DialogService DialogService { get; set; } = default!;
        [Inject] CreateConsolidatedHttp CreateConsolidatedHttp { get; set; } = default!;
        [Inject] CustomDialogService CustomDialogService { get; set; } = default!;
        [Inject] NavigationManager NavigationManager { get; set; } = default!;
        [Inject] IJSRuntime JS { get; set; } = default!;

        [Parameter] public Consolidated? consolidated { get; set; }

        bool loading = false;
        bool busy = false;

        public async Task SubmitAsync()
        {
            loading = busy = true;

            List<int> packageIds = consolidated!.ConsolidatedDetail.Select(d => d.PackageId).ToList();

            ConsolidatedResponseDTO response = new();
            if (consolidated!.Type == 1)
            {
                ConsolidatedSenderRequestDTO SenderDTO = new();
                SenderDTO.ConsolidatedType = consolidated.Type;
                SenderDTO.PackagesIds = packageIds;
                
                response = await CreateConsolidatedHttp.SendSenderConsolidationAsync(SenderDTO);
            }
            else if (consolidated!.Type == 2)
            {
                ConsolidatedCorrespondenceRequestDTO CorrespondenceDTO = new();
                CorrespondenceDTO.ConsolidatedType = consolidated.Type;
                CorrespondenceDTO.RecipientLocationId = consolidated.RecipientLocationId;
                CorrespondenceDTO.PackagesIds = packageIds;

                response = await CreateConsolidatedHttp.SendCorrespondenceConsolidationAsync(CorrespondenceDTO);
            }

            if (response.ConsolidatedId > 0)
            {
                await SuccessAsync(response);
            }

            loading = busy = false;
        }

        private async Task SuccessAsync(ConsolidatedResponseDTO response)
        {
            var redirect = await DialogService.Alert($"Consolidado <strong>{response.ConsolidatedId}</strong> creado con éxito", "Operación exitosa", new AlertOptions { CloseDialogOnEsc = false, CloseDialogOnOverlayClick = false, OkButtonText = "Aceptar", ShowClose = false });
            
            if (redirect == true && consolidated!.Type == 1)
            {
                NavigationManager.NavigateTo($"/consolidados/rastrear/{response.ConsolidatedId}");
            }

            if (redirect == true && consolidated!.Type == 2)
            {
                NavigationManager.NavigateTo($"/correspondencia/rastrear_consolidado/{response.ConsolidatedId}");
            }
        }

        public void Cancel()
        {
            DialogService.Close();
        }
    }
}