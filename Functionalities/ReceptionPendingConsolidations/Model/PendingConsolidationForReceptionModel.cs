using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.ReceptionPendingConsolidations.Model;
public class PendingConsolidationForReceptionModel
{
    [JsonPropertyName("IdConsolidado")]
    public int ConsolidatedId { get; set; }

    [JsonPropertyName("UbicacionDestino")]
    public string? SenderLocation { get; set; }

    [JsonPropertyName("CreadoPor")]
    public string? Sender { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime? Date { get; set; }
}