using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Model;
public class ConsolidationTrackingForm
{
    [Required(ErrorMessage = "El número de consolidado no es válido.")]
    public int? ConsolidationId { get; set; }
}