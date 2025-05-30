using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Session;
public class SessionUser
{
    [JsonPropertyName("CodigoEmpleado")]
    public required string EmployeeCode { get; set; }

    [JsonPropertyName("Nombre")]
    public required string Name { get; set; }

    [JsonPropertyName("Usuario")]
    public required string User { get; set; }

    [JsonPropertyName("Rol")]
    public required string Rol { get; set; }

    [JsonPropertyName("UbicacionId")]
    public int? LocationId { get; set; }

    [JsonPropertyName("IdTienda")]
    public int? StoreId { get; set; }

    [JsonPropertyName("Tienda")]
    public string? Store { get; set; }

    [JsonPropertyName("Menu")]
    public List<Menu>? Menu { get; set; }
}