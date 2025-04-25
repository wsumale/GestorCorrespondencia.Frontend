using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Model.Auth.SolicitudAcceso;
public class FormularioValidarCodigo
{
    [Required(ErrorMessage = "Codigo requerido")]
    public int? employeeCode { get; set; }
}