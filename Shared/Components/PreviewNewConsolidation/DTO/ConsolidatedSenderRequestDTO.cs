using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Shared.Components.PreviewNewConsolidation.DTO;
public class ConsolidatedSenderRequestDTO
{

    [JsonPropertyName("TipoConsolidado")]
    public int ConsolidatedType { get; set; }

    [JsonPropertyName("IdsPaquete")]
    public List<int> PackagesIds { get; set; }
}