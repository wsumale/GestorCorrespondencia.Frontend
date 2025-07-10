using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.PackageShipping.Model;
public class PackageDetailTypeItem
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("Descripcion")]
    public string? Description { get; set; }
}