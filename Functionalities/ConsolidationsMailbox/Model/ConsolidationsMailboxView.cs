using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationsMailbox.Model;
public class ConsolidationsMailboxView
{
    [JsonPropertyName("IdConsolidado")]
    public int ConsolidatedId { get; set; }

    [JsonPropertyName("UbicacionOrigen")]
    public string? SenderLocation { get; set; }

    [JsonPropertyName("UbicacionDestino")]
    public string? DestinationLocation { get; set; }

    [JsonPropertyName("CreadoPor")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime CreatedAt { get; set; }
}