using System.Text.Json;
using GestorCorrespondencia.Frontend.Functionalities.PendingPackages.DTO;
using GestorCorrespondencia.Frontend.Functionalities.PendingPackages.Http;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Security;
using GestorCorrespondencia.Frontend.Services.SGU;
using GestorCorrespondencia.Frontend.Shared.Model;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.PendingPackages.Components;
public partial class ChangeReceiverInDestination
{
    [Inject] PendingPackagesHttp PendingPackagesHttp { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;
    [Inject] DialogService DialogService { get; set; } = default!;
    [Inject] SGUService SGUService { get; set; } = default!;
    [Inject] GetCurrentUser GetCurrentUser { get; set; } = default!;

    [Inject] ILogger<ChangeReceiverInDestination> _logger { get; set; } = default!;

    [Parameter] public int PackageId { get; set; }

    private bool loading = false;

    private ChangeReceiverInDestinationDTO Form = new();

    private IList<User> Users = new List<User>();
    
    protected override async Task OnInitializedAsync()
    {
        loading = true;

        var user = await GetCurrentUser.GetUserInfoAsync();
        Users = await SGUService.GetUsersPhysicalLocationFromUserLocationAsync(user.LocationId ?? 0);

        Form.RelationshipType = 1;
        Form.IncidentTypeId = 4;
        Form.PackageId = PackageId;

        loading = false;
        StateHasChanged();
    }

    private async Task OnSubmitChangeReceiverAsync()
    {
        bool confirm = await CustomDialogService.OpenConfirmAsync("Esta a punto de modificar el destinatario ┐Continuar?", "Confirmar", "Cambiar destinatario", "Cancelar", new DialogOptions { Width = "400px" });
        if (confirm)
        {
            StateHasChanged();

            loading = true;
            await PendingPackagesHttp.ChangeReceiverAsync(Form);
            loading = false;

            DialogService.Close();
        }
    }

    private void OnReceiverChanged(object value)
    {
        var selectedUser = Users.FirstOrDefault(u => u.Id.Equals(value));
        if (selectedUser != null)
        {
            Form.NewDestinationId = selectedUser.LocationId ?? 0;
        }
    }


}