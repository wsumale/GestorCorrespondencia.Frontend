using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
public class PackageDetail
{
    [JsonPropertyName("TipoItem")]
    public string? Type { get; set; }

    [JsonPropertyName("Comentarios")]
    public string? Comment { get; set; }

    [JsonPropertyName("Cantidad")]
    public int Quantity { get; set; }
}