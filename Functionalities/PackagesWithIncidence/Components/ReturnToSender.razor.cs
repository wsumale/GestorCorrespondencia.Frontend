using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.DTO;
using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Http;
using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Components;
public partial class ReturnToSender
{
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] PackagesWithIncidentHttp PackagesWithIncidentHttp { get; set; } = default!;
    [Inject] DialogService DialogService { get; set; } = default!;

    [Parameter] public int IncidentId { get; set; }
    private ReturnToSenderDTO Form = new(); 

    private bool loading = false;

    private async Task OnSubmitReturnedToSenderAsync()
    {
        bool confirm = await CustomDialogService.OpenConfirmAsync("Esta a punto de devolver el paquete al remitente. ¿Continuar?", "Confirmar", "Devolver al remitente", "Cancelar", new DialogOptions { Width = "400px" });

        if (confirm)
        {
            StateHasChanged();
            loading = true;
            await PackagesWithIncidentHttp.ResolveReturnToSenderIncidentAsync(IncidentId, Form, "Paquete devuelto al remitente");
            loading = false;
            DialogService.Close();
        }
    }
}