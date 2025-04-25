using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Unisuper.GestorCorrespondencia.Frontend.Model
{
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

    public class DetalleEnvio
    {
        public string? Tipo { get; set; }
        public string? Comentarios { get; set; }
        public int Cantidad { get; set; }
    }
}
