using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Model;
public class PackageWithIncident
{
    [JsonPropertyName("IdIncidencia")]
    public int IncidentId { get; set; }

    [JsonPropertyName("IdDetalleConsolidado")]
    public int? ConsolidatedDetailId { get; set; }

    [JsonPropertyName("IdPaquete")]
    public int? PackageId { get; set; }

    [JsonPropertyName("IdTipoIncidencia")]
    public int? IncidentTypeId { get; set; }

    [JsonPropertyName("TipoIncidencia")]
    public string? IncidentType { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("CreadoPor")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("Solucionada")]
    public bool Solved { get; set; }
}