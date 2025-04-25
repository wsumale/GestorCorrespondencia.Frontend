using System.ComponentModel.DataAnnotations;

namespace Unisuper.GestorCorrespondencia.Frontend.Model
{
    public class FormularioLogin
    {
        [Required(ErrorMessage = "Email requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contraseña requerida")]
        public string Password { get; set; }
    }
}
