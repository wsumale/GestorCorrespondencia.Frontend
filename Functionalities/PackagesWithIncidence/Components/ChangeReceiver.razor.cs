using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.DTO;
using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Http;
using GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.SGL;
using GestorCorrespondencia.Frontend.Services.SGU;
using GestorCorrespondencia.Frontend.Shared.Model;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Components;
public partial class ChangeReceiver
{
    [Inject] SGLService SGLService { get; set; } = default!;
    [Inject] SGUService SGUService { get; set; } = default!;
    [Inject] PackagesWithIncidentHttp PackagesWithIncidentHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] DialogService DialogService { get; set; } = default!;

    private bool loading = false;
    private bool busy = false;
    private IList<Location>? Locations;
    private IList<User>? Users;

    [Parameter] public int IncidentId { get; set; }
    private IncidentResolveDTO form = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadLocationsAsync();
    }

    private async Task OnSubmitChangeRecipientAsync()
    {
        if (form.NewRecipientId > 0 && form.Solution!.Any())
        {
            bool confirm = await CustomDialogService.OpenConfirmAsync("Esta a punto de modificar el destinatario. ¿Continuar?", "Confirmar", "Cambiar destinatario", "Cancelar", new DialogOptions { Width = "400px" });

            if (confirm)
            {
                StateHasChanged();
                loading = busy =true;
                await PackagesWithIncidentHttp.ResolveChangeOfRecipientIncidentAsync(IncidentId, form, "Destinatario actualizado");
                loading = busy = false;
                DialogService.Close();
            }
        }
    }

    private async Task LoadLocationsAsync()
    {
        loading = true;
        Locations = await SGLService.GetLocationsSendPackagesAsync();
        loading = false;
        StateHasChanged();
    }

    private async Task GetUsersByLocationIdAsync()
    {
        loading = true;
        Users = await SGUService.GetUsersByLocationAsync(form.NewRecipientLocationId ?? 0, false);
        loading = false;
        StateHasChanged();
    }

}