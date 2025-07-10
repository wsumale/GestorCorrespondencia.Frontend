using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.DTO;
public class PackageResponseDTO
{
    [JsonPropertyName("IdPaquete")]
    public int PackageId { get; set; }
}