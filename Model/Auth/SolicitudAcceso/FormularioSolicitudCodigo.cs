using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Model.Auth.SolicitudAcceso;
public class FormularioSolicitudCodigo
{
    [Required(ErrorMessage = "Codigo requerido")]
    public int? Codigo { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    public string? RepeatPassword { get; set; }
}
