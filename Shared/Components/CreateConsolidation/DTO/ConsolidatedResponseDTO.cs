using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.DTO;
public class ConsolidatedResponseDTO
{
    [JsonPropertyName("IdConsolidado")]
    public int ConsolidatedId { get; set; }
}