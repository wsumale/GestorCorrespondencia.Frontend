using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Model.Auth.RecuperarPassword;
public class FormularioReiniciarPassword
{
    [Required(ErrorMessage = "Codigo requerido")]
    public int? Codigo { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Contraseña requerida")]
    public string? RepeatPassword { get; set; }

    public bool samePasswords()
    {
        return this.Password == this.RepeatPassword;
    }
}