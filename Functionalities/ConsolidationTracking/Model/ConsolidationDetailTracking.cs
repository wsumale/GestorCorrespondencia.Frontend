using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.ConsolidationTracking.Model;
public class ConsolidationDetailTracking
{
    [JsonPropertyName("IdDetalleConsolidado")]
    public int ConsolidatedDetailId { get; set; }

    [JsonPropertyName("IdPaquete")]
    public int PackageId { get; set; }

    [JsonPropertyName("Remitente")]
    public string? Sender { get; set; }

    [JsonPropertyName("UbicacionRemitente")]
    public string? SenderLocation { get; set; }

    [JsonPropertyName("Destinatario")]
    public string? Addressee { get; set; }

    [JsonPropertyName("UbicacionDestinatario")]
    public string? DestinationLocation { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("IdEstado")]
    public int StateId { get; set; }

    [JsonPropertyName("Estado")]
    public string? State { get; set; }
}