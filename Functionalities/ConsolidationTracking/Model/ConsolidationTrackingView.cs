using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Model;
public class ConsolidationTrackingView
{
    [JsonPropertyName("IdConsolidado")]
    public int ConsolidatedId { get; set; }

    [JsonPropertyName("UbicacionOrigen")]
    public string? SenderLocation { get; set; }

    [JsonPropertyName("UbicacionDestino")]
    public object? DestinationLocation { get; set; }

    [JsonPropertyName("CreadoPor")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("Recibido")]
    public bool Received { get; set; }

    [JsonPropertyName("Paquetes")]
    public List<ConsolidationDetailTracking>? Detail { get; set; } = new();
}