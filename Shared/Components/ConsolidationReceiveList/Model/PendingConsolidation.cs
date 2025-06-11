using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Shared.Components.ConsolidationReceiveList.Model;
public class PendingConsolidation
{
    [JsonPropertyName("IdConsolidado")]
    public int ConsolidatedId { get; set; }

    [JsonPropertyName("UbicacionDestino")]
    public string? DestinationLocation { get; set; }

    [JsonPropertyName("CreadoPor")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("Paquetes")]
    public List<PendingConsolidationDetail>? Detail { get; set; }
}