using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.DTO;
using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Mapper;
using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Services.Http;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Components;
public partial class WBAccessRequestExistingUser
{
    [Inject] private ApiPostService ApiPostService { get; set; } = default!;
    [Inject] private DialogService DialogService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private CustomDialogService CustomDialogService { get; set; } = default!;

    [Parameter] public WBValidateEmployeeCodeForm? ValidateEmployeeCode { get; set; }

    bool readOnlyEmployeeCode = true;
    public WBAccessRequestExistingUserForm Form = new();
    public AccessRequestDTO Request = new();
    bool busy;

    protected override void OnInitialized()
    {
        Form.EmployeeCode = ValidateEmployeeCode!.EmployeeCode;
        readOnlyEmployeeCode = ValidateEmployeeCode.EmployeeCode == null || ValidateEmployeeCode.EmployeeCode == 0 ? false : true;
    }

    private async Task OnSubmitAsync()
    {
        busy = true;

        Request = Form.WBAccessRequestExistingUserFormToAccessRequestDTO();

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

    private void ToLogin()
    {
        NavigationManager.NavigateTo("/");
    }
}