using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Form;
public class LoginForm
{
    [Required(ErrorMessage = "Usuario requerido")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    public string? Password { get; set; }

    public string? AuthenticatorCode { get; set; }
}
