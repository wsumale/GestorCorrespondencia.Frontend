using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
public class PackageActionLog
{
    [JsonPropertyName("EstadoPaqueteId")]
    public int PackageStateId { get; set; }

    [JsonPropertyName("EstadoPaquete")]
    public string? PackageState { get; set; }

    [JsonPropertyName("CreadorPor")]        
    public string? CreatedBy { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("Descripcion")]
    public string? Description { get; set; }
}