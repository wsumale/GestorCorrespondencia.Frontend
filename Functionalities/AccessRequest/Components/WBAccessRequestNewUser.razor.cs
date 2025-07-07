using Microsoft.AspNetCore.Components;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;
using GestorCorrespondencia.Frontend.Services.Security;
using System.Text.Json;
using GestorCorrespondencia.Frontend.Shared.Components;
using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.DTO;
using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Model;
using GestorCorrespondencia.Frontend.Shared.Model;
using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Mapper;
using GestorCorrespondencia.Frontend.Services.Dialogs;

namespace GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Components;
public partial class WBAccessRequestNewUser
{
    [Inject] private ApiPostService ApiPostService { get; set; } = default!;
    [Inject] private DialogService DialogService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private CustomDialogService CustomDialogService { get; set; } = default!;

    [Parameter] public WBValidateEmployeeCodeForm? ValidateEmployeeCode { get; set; }

    bool readOnlyCodigo = true;

    public WBAccessRequestNewUserForm Form = new();
    public AccessRequestDTO Request = new();
    bool busy;

    protected override void OnInitialized()
    {
        Form.EmployeeCode = ValidateEmployeeCode!.EmployeeCode;
        readOnlyCodigo = ValidateEmployeeCode.EmployeeCode == null || ValidateEmployeeCode.EmployeeCode == 0 ? false : true;
    }

    private async Task OnSubmitAsync()
    {
        busy = true;

        Request = Form.WBAccessRequestNewUserFormTOAccessRequestDTO();

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
                await CustomDialogService.OpenViewErrorsAsync(response);
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