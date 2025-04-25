using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Model.Auth;
public class FormularioLogin
{
    [Required(ErrorMessage = "Email requerido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    public string Password { get; set; }
}
