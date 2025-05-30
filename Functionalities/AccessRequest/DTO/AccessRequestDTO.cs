using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.AccessRequest.DTO;
public class AccessRequestDTO
{
    [property: JsonPropertyName("IdTipoAutenticacion")]
    public int AuthenticationTypeId { get; set; }

    [property: JsonPropertyName("IdTienda")]
    public int StoreId { get; set; }

    [property: JsonPropertyName("CodigoSistema")]
    public string? SystemCode { get; set; }

    [property: JsonPropertyName("Username")]
    public string? Username { get; set; }

    [property: JsonPropertyName("Password")]
    public string? Password { get; set; }
}