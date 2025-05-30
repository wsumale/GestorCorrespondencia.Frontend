using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Functionalities.AccessRequest.Model;
public class ADAccessRequestForm
{
    [Required(ErrorMessage = "Usuario requerido")]
    [EmailAddress(ErrorMessage = "La dirección de correo no es válida")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    public string? Password { get; set; }
}