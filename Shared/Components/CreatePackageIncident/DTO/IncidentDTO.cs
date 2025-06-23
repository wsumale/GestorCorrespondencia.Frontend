using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Shared.Components.CreatePackageIncident.DTO;
public class IncidentDTO
{
    [JsonPropertyName("IdDetalleConsolidacion")]
    public int ConsolidatedDetailId { get; set; }

    [JsonPropertyName("IdTipoIncidencia")]
    public int? IncidentTypeId { get; set; }

    [JsonPropertyName("Comentarios")]
    public string? Comment { get; set; }
}