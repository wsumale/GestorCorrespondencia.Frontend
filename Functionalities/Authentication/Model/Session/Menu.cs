using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Session;
public class Menu
{
    [JsonPropertyName("Seccion")]
    public string? Section { get; set; }

    [JsonPropertyName("Modulo")]
    public string? Module { get; set; }

    [JsonPropertyName("Icono")]
    public string? Icon { get; set; }

    [JsonPropertyName("Ruta")]
    public string? Path { get; set; }

    [JsonPropertyName("Permisos")]
    public Permissions? Permissions { get; set; }
}