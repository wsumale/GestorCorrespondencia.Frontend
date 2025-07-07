using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.DTO;
public class IncidentResolveDTO
{
    [JsonPropertyName("Solucion")]
    [Required(ErrorMessage = "Este campo es requerido")]
    public string? Solution { get; set; }

    [JsonPropertyName("IdNuevoDestinatario")]
    [Required(ErrorMessage = "Debe seleccionar un usuario")]
    public int? NewRecipientId { get; set; }
}