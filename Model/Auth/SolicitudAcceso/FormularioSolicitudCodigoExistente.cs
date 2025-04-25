using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Model.Auth.SolicitudAcceso;
public class FormularioSolicitudCodigoExistente
{
    [Required(ErrorMessage = "Codigo requerido")]
    public int? Codigo { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    public string? Password { get; set; }
}