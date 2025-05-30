using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Http;
using Microsoft.AspNetCore.Components;
using Radzen;
using GestorCorrespondencia.Frontend.Shared.Components;
using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.DTO;
using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Mapper;
using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Model;
using GestorCorrespondencia.Frontend.Shared.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;

namespace GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Pages;
public partial class ADAccessRequest
{
    [Inject] private ApiPostService ApiPostService { get; set; } = default!;
    [Inject] private DialogService DialogService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private ILogger<ADAccessRequest> _logger { get; set; } = default!;
    [Inject] private CustomDialogService CustomDialogService { get; set; } = default!;

    public ADAccessRequestForm Form = new();
    public AccessRequestDTO Request = new();
    bool busy;

    private async Task OnSubmit()
    {
        busy = true;

        Request = Form.ADAccessRequestFormToAccessRequestDTO();

        int StatusCode = 0;

        try
        {
            var response = await ApiPostService.PostAsync("auth/solicitud", Request, 2, false);

            if (response.IsSuccessStatusCode)
            {
                await DialogService.Alert("La solicitud ha sido creada con éxito.\nPendiente de aprobación.", "Operación exitosa", new AlertOptions() { OkButtonText = "Aceptar", });
                StatusCode = (int)response.StatusCode;
            }
            else
            {
                await CustomDialogService.OpenViewErrors(response);
                StatusCode = (int)response.StatusCode;
            }
        }
        catch (Exception e)
        {
            await DialogService.Alert(e.Message, "Error de Conexión", new AlertOptions() { OkButtonText = "Aceptar" });
        }
        finally
        {
            if (StatusCode != 401)
            {
                ToLogin();
            }
            busy = false;
        }
    }

    public void ToLogin()
    {
        NavigationManager.NavigateTo("/");
    }
}