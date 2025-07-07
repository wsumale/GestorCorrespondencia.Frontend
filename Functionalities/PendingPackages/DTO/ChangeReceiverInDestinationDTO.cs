using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.PendingPackages.DTO;
public class ChangeReceiverInDestinationDTO
{
    [JsonPropertyName("IdTipoIncidencia")]
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int IncidentTypeId { get; set; }

    [JsonPropertyName("Comentarios")]
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Comment { get; set; }

    [JsonPropertyName("TipoRelacion")]
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int RelationshipType { get; set; }

    [JsonPropertyName("IdPaquete")]
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int PackageId { get; set; }

    [JsonPropertyName("IdNuevoDestinatario")]
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public int NewReceiverId { get; set; }

    [JsonPropertyName("IdNuevoDestino")]
    [Required(ErrorMessage = "Esta campo es obligatorio.")]
    public int NewDestinationId { get; set; }
}