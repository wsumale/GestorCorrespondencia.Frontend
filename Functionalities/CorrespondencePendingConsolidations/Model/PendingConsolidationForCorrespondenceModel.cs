using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.CorrespondencePendingConsolidations.Model;
public class PendingConsolidationForCorrespondenceModel
{
    [JsonPropertyName("IdConsolidado")]
    public int ConsolidatedId { get; set; }

    [JsonPropertyName("UbicacionOrigen")]
    public string? SenderLocation { get; set; }

    [JsonPropertyName("CreadoPor")]
    public string? Sender { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime? Date { get; set; }
}