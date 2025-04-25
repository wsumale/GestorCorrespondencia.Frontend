using System.ComponentModel.DataAnnotations;

namespace GestorCorrespondencia.Frontend.Model.Paquetes.Nuevo_Envio;
public class FormularioEnvio
{
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? CategoriaDestino { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Destino { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? EmailDestinatario { get; set; }
    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? NombreDestinatario { get; set; }
    public string? Observaciones { get; set; }
    public List<DetalleEnvio> Detalles { get; set; } = new List<DetalleEnvio>();

    public bool PuedeEnviar =>
        !string.IsNullOrEmpty(EmailDestinatario) &&
        new EmailAddressAttribute().IsValid(EmailDestinatario) &&
        !string.IsNullOrEmpty(NombreDestinatario) &&
        !string.IsNullOrEmpty(CategoriaDestino) &&
        !string.IsNullOrEmpty(Destino);
}
