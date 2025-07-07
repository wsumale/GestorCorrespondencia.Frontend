using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Shared.Components.CreatePackageIncident.DTO;
public class IncidentDTO
{
    [JsonPropertyName("IdDetalleConsolidado")]
    public int ConsolidatedDetailId { get; set; }

    [JsonPropertyName("IdTipoIncidencia")]
    public int? IncidentTypeId { get; set; }

    [JsonPropertyName("TipoRelacion")]
    public int RelationshipType { get; set; }

    [JsonPropertyName("Comentarios")]
    public string? Comment { get; set; }
}