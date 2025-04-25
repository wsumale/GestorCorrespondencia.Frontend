using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Model.Auth.RecuperarPassword;
public class FormularioSolicitudReiniciaPassword
{
    [Required(ErrorMessage = "Codigo requerido")]
    public int? Codigo { get; set; }
}