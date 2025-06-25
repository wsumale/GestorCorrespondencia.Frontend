using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
public class Package
{
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
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("IdEstado")]
    public int StateId { get; set; }

    [JsonPropertyName("Estado")]
    public string? State { get; set; }

    [JsonPropertyName("Observaciones")]
    public string? Observations { get; set; }

    [JsonPropertyName("Detalle")]
    public List<PackageDetail> Details { get; set; } = new List<PackageDetail>();

}