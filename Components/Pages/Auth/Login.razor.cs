using Microsoft.AspNetCore.Components;
using Radzen;
using Unisuper.GestorCorrespondencia.Frontend.Model;
using Unisuper.GestorCorrespondencia.Frontend.Model.Responses;
namespace Unisuper.GestorCorrespondencia.Frontend.Components.Pages.Auth
{
    public partial class Login
    {
        [Inject] protected NotificationService NotificationService { get; set; } = default!;

        private FormularioLogin usuario = new();
        private string? error;
        private bool busy;

        private async Task OnSubmit()
        {
            //try
            //{
            //    busy = true;
            //    var (data, err) = await Api.GetAsync<ApiResponse<UsuarioSesion>, UsuarioSesion, ApiResponse<UsuarioSesion>>(
            //        $"seguridad/login?user={usuario.Email}&password={usuario.Password}", 1);

            //    if (err != null)
            //    {
            //        error = err;

            //        NotificationService.Notify(new NotificationMessage
            //        {
            //            Severity = NotificationSeverity.Error,
            //            Summary = "Error de inicio de sesión",
            //            Detail = err,
            //            Duration = 4000
            //        });

            //        return;
            //    }

            //    if (data != null)
            //    {
            //        await Sesion.GuardarSesion(data);
            //        NavigationManager.NavigateTo("/home", forceLoad: true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    NotificationService.Notify(new NotificationMessage
            //    {
            //        Severity = NotificationSeverity.Error,
            //        Summary = "Excepción no controlada",
            //        Detail = ex.Message,
            //        Duration = 5000
            //    });
            //}
            //finally
            //{
            //    busy = false;
            //}
        }
    }
}
