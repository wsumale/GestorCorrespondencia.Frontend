using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Model;
public class WBAccessRequestNewUserForm
{
    [Required(ErrorMessage = "Codigo requerido")]
    public int? EmployeeCode { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
    ErrorMessage = "La contraseña debe tener al menos 8 caracteres, una letra mayúscula, una minúscula, un número y un carácter especial.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Repite tu contraseña")]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    public string? RepeatPassword { get; set; }
}
