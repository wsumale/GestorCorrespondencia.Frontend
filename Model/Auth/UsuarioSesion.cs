namespace GestorCorrespondencia.Frontend.Model.Auth;
public class UsuarioSesion
{
    public int id { get; set; }
    public string nombre { get; set; }
    public string usuario { get; set; }
    public string clave { get; set; }
    public string token { get; set; }
    public DateTime expiracion_token { get; set; }
    public DateTime creado_en { get; set; }
    public DateTime actualizado_en { get; set; }
}