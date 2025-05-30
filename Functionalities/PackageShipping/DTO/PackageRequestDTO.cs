using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.DTO;
public class PackageRequestDTO
{
    [JsonPropertyName("Destinatario")]
    public int Addressee { get; set; }

    [JsonPropertyName("UbicacionDestinatario")]
    public int RecipientLocation { get; set; }

    [JsonPropertyName("Observaciones")]
    public string? Observations { get; set; }

    [JsonPropertyName("DetallePaquete")]
    public List<PackageDetailsRequestDTO>? PackageDetails { get; set; }
}