using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
public class PackageTrackingForm
{
    [Required(ErrorMessage = "El número de paquete no es válido.")]
    public int? PackageId { get; set; }
}