using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.DTO;
public class TwoFactorSettingsResponseDto
{
    [JsonPropertyName("qrCodeUri")]
    public string? qrCodeUri { get; set; }

    [JsonPropertyName("secretKey")]
    public string? secretKey { get; set; }
}