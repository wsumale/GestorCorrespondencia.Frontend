namespace GestorCorrespondencia.Frontend.Functionalities.Tracking.Model;
public class Package
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
    public List<PackageDetail> Detalles { get; set; } = new List<PackageDetail>();

}