using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Shared.Components.CreatePackageIncident.Model;
public class IncidentFormModel
{
    [Required(ErrorMessage = "Debe seleccionar el tipo de incidencia")]
    public int? IncidentType { get; set; }

    [Required(ErrorMessage = "Debe agregar una descripción")]
    public string? Comment { get; set; }
}