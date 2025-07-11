using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
public class PackageActionLog
{
    [JsonPropertyName("EstadoPaqueteId")]
    public int PackageStateId { get; set; }

    [JsonPropertyName("EstadoPaquete")]
    public string? PackageState { get; set; }

    [JsonPropertyName("CreadoPor")]        
    public string? CreatedBy { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("Descripcion")]
    public string? Description { get; set; }

    [JsonPropertyName("IdConsolidado")]
    public int? ConsolidationId { get; set; }
}