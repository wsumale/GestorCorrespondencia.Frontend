using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.SenderConsolidation.Model;
public class SenderConsolidationView
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
    public string? RecipientLocation { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime Created { get; set; }

    [JsonPropertyName("Estado")]
    public int State { get; set; }
}