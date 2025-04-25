using System.ComponentModel.DataAnnotations;

namespace Unisuper.GestorCorrespondencia.Frontend.Model.SolicitudAcceso
{
    public class FormularioValidarCodigo
    {
        [Required(ErrorMessage = "Codigo requerido")]
        public int? employeeCode { get; set; }
    }

    public class FormularioSolicitudCodigo
    {
        [Required(ErrorMessage = "Codigo requerido")]
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        public string? RepeatPassword { get; set; }
    }

    public class FormularioSolicitudCodigoExistente
    {
        [Required(ErrorMessage = "Codigo requerido")]
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        public string? Password { get; set; }
    }
}
