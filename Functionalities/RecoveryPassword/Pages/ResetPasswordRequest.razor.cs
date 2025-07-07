using Microsoft.AspNetCore.Components;
using GestorCorrespondencia.Frontend.Services.Http;
using Radzen;
using GestorCorrespondencia.Frontend.Functionalities.RecoveryPassword.Model;
using GestorCorrespondencia.Frontend.Services.Dialogs;
using GestorCorrespondencia.Frontend.Extensions;

namespace GestorCorrespondencia.Frontend.Functionalities.RecoveryPassword.Pages;
public partial class ResetPasswordRequest
{
    [Inject] ApiPostService ApiPostService { get; set; } = default!;
    [Inject] DialogService DialogService { get; set; } = default!;
    [Inject] NavigationManager NavigationManager { get; set; } = default!;
    [Inject] CustomDialogService CustomDialogService { get; set; } = default!;

    public ResetPasswordRequestForm Request = new();
    public bool busy;

    public async Task OnSubmit()
    {
        try
        {
            busy = true;

            var response = await ApiPostService.PostAsync($"auth/recuperar-credenciales?employeeCode={Request.EmployeeCode}", Request, 2, false);

            if (response.IsSuccessStatusCode)
            {
                var close = await DialogService.Alert("Solicitud realizada con éxito", "Operación éxitosa", new AlertOptions { OkButtonText = "Aceptar", CloseDialogOnEsc = false, ShowClose = false, CloseDialogOnOverlayClick = false });
                if (close == true)
                {
                    NavigationManager.NavigateTo("/", forceLoad: true);
                }
            }
            else
            {
                await CustomDialogService.OpenViewErrorsAsync(response);
            }
        }
        catch (Exception e)
        {
            await DialogService.Alert(e.Message, "Error Innesperado", new AlertOptions() { OkButtonText = "Aceptar" });
        }
        finally
        {
            busy = false;
        }
    }

}