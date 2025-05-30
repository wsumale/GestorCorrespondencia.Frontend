using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Form;
public class TwoFactorAuthFormModel
{
    [Required(ErrorMessage = "Código requerido")]
    public int? AuthenticatorCode { get; set; }
}