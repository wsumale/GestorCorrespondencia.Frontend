using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.DTO;
public class PackageDetailsRequestDTO
{
    [JsonPropertyName("IdTipoItem")]
    public int TypeItemId { get; set; }

    [JsonPropertyName("Cantidad")]
    public int Quantity { get; set; }

    [JsonPropertyName("Comentarios")]
    public string? Comments { get; set; }
}