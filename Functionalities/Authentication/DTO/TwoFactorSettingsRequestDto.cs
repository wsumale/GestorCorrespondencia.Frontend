using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.DTO;
public class TwoFactorSettingsRequestDto
{
    [JsonPropertyName("username")]
    public string? username { get; set; }

    [JsonPropertyName("authenticatorCode")]
    public int? authenticatorCode { get; set; }

    [JsonPropertyName("secretKey")]
    public string? secretKey { get; set; }
}