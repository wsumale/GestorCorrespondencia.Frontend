using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Shared.Components.ViewConsolidationDetail.Model;
public class Consolidation
{
    [JsonPropertyName("IdConsolidado")]
    public int ConsolidatedId { get; set; }

    [JsonPropertyName("UbicacionOrigen")]
    public string? SenderLocation { get; set; }

    [JsonPropertyName("CreadoPor")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("Paquetes")]
    public List<ConsolidationDetail>? Detail { get; set; }

    [JsonPropertyName("Recibido")]
    public bool Received { get; set; }
}