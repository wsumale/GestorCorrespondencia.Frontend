using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestorCorrespondencia.Frontend.Functionalities.PackagesWithIncidence.DTO;
public class ReturnToSenderDTO
{
    [JsonPropertyName("Solucion")]
    [Required(ErrorMessage = "Este campo es requerido")]
    public string? Solution { get; set; }
}