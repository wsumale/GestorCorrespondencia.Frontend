using System.Text.Json;
using GestorCorrespondencia.Frontend.Services.Http;
using GestorCorrespondencia.Frontend.Shared.Components;
using Radzen;
using Microsoft.AspNetCore.Components;
using GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Model;
using GestorCorrespondencia.Frontend.Shared.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;

namespace GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Pages;
public partial class WBAccessRequest
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private ApiGetService ApiGetService { get; set; } = default!;
    [Inject] private DialogService DialogService { get; set; } = default!;
    [Inject] private CustomDialogService CustomDialogService { get; set; } = default!;

    private int formulario = 0; //0=Validar Codigo, 1=Usar Credenciales Existentes, 2=Crear Usuario
    public readonly WBValidateEmployeeCodeForm Form = new WBValidateEmployeeCodeForm();
    bool busy;

    private async Task OnSubmitValidateCodeAsync()
    {
        busy = true;

        try
        {
            var response = await ApiGetService.GetAsync($"auth/validar-workbeat?employeeCode={Form.EmployeeCode}", "", 2, false);

            if (response.IsSuccessStatusCode)
            {
                switchForm((int)response.StatusCode);
            }
            else
            {
                await CustomDialogService.OpenViewErrorsAsync(response);
                switchForm((int)response.StatusCode);
            }
        }
        catch (Exception e)
        {
            await DialogService.Alert(e.Message, "Error de Conexión", new AlertOptions() { OkButtonText = "Aceptar" });
        }
        finally
        {
            busy = false;
        }
    }

    void switchForm(int? i)
    {
        switch (i)
        {
            case 200:
                formulario = 1;
                break;
            case 406:
                formulario = 2;
                break;
            default:
                formulario = 0;
                break;
        }
    }
}