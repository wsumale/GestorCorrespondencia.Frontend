using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Shared.Model;
public class Location
{
    [JsonPropertyName("IdUbicacion")]
    public required int LocationId { get; set; }

    [JsonPropertyName("Codigo")]
    public required string LocationCode { get; set; }

    [JsonPropertyName("Nombre")]
    public required string LocationName { get; set; }

    [JsonPropertyName("Estado")]
    public required bool Status { get; set; }
}