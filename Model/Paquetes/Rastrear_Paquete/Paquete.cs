namespace Unisuper.GestorCorrespondencia.Frontend.Model
{
    public class Paquete
    {

        public string? NumeroRastreo { get; set; }
        public string? UbicacionOrigen { get; set; }
        public string? Origen { get; set; }
        public string? UbicacionDestino { get; set; }
        public string? Destino { get; set; }
        public string? EmailRemitente { get; set; }
        public string? NombreRemitente { get; set; }
        public string? EmailDestinatario { get; set; }
        public string? NombreDestinatario { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? Fecha_Envio { get; set; }
        public List<DetallePaquete> Detalles { get; set; } = new List<DetallePaquete>();

    }

    public class DetallePaquete
    {
        public string? Tipo { get; set; }
        public string? Comentarios { get; set; }
        public int Cantidad { get; set; }
    }

}
