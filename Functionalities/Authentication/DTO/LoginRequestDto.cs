using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.DTO;
public class LoginRequestDto
{
    [JsonPropertyName("IdTienda")]
    public int StoreId { get; init; } = 0;

    [JsonPropertyName("CodigoSistema")]
    public string? SystemCode { get; init; } = "SGCRS";

    [JsonPropertyName("Username")]
    public string? Username { get; set; }

    [JsonPropertyName("Password")]
    public string? Password { get; set; }

    [JsonPropertyName("AuthenticatorCode")]
    public string? AuthenticatorCode { get; set; }
}