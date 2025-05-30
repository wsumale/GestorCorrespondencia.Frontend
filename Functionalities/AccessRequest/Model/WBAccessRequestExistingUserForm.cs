using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Model;
public class WBAccessRequestExistingUserForm
{
    [Required(ErrorMessage = "Código requerido")]
    public int? EmployeeCode { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    public string? Password { get; set; }
}