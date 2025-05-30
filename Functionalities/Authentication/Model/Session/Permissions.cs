using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.Authentication.Model.Session;
public class Permissions
{
    [JsonPropertyName("Creación")]
    public bool Create { get; set; }

    [JsonPropertyName("Lectura")]
    public bool Read { get; set; }

    [JsonPropertyName("Actualización")]
    public bool Update { get; set; }

    [JsonPropertyName("Eliminación")]
    public bool Delete { get; set; }
}