using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Shared.Components.CreateConsolidation.DTO;

public class ConsolidatedCorrespondenceRequestDTO
{
    [JsonPropertyName("IdUbicacionDestino")]
    public int RecipientLocationId { get; set; }

    [JsonPropertyName("TipoConsolidado")]
    public int ConsolidatedType { get; set; }

    [JsonPropertyName("IdsPaquete")]
    public List<int> PackagesIds { get; set; }
}