using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.Model;
public class Incident
{
    [JsonPropertyName("Remitente")]
    public string? Sender { get; set; }

    [JsonPropertyName("UbicacionRemitente")]
    public string? SenderLocation { get; set; }

    [JsonPropertyName("Destinatario")]
    public string? Addressee { get; set; }

    [JsonPropertyName("UbicacionDestinatario")]
    public string? DestinationLocation { get; set; }

    [JsonPropertyName("IdEstado")]
    public int StateId { get; set; }

    [JsonPropertyName("Estado")]
    public string? State { get; set; }

    [JsonPropertyName("Comentarios")]
    public string? Comment { get; set; }

    [JsonPropertyName("Solucion")]
    public string? Solution { get; set; }

    [JsonPropertyName("FechaSolucion")]
    public DateTime? SolvedDate { get; set; }

    [JsonPropertyName("SolucionadoPor")]
    public string? SolvedBy { get; set; }

    [JsonPropertyName("IdIncidencia")]
    public int IncidentId { get; set; }

    [JsonPropertyName("IdDetalleConsolidado")]
    public int? ConsolidatedDetailId { get; set; }

    [JsonPropertyName("IdPaquete")]
    public int? PackageId { get; set; }

    [JsonPropertyName("IdTipoIncidencia")]
    public int IncidentTypeId { get; set; }

    [JsonPropertyName("TipoIncidencia")]
    public string? IncidentType { get; set; }

    [JsonPropertyName("FechaCreacion")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("CreadoPor")]
    public string? CreatedBy { get; set; }

    [JsonPropertyName("Solucionada")]
    public bool Solved { get; set; }
}