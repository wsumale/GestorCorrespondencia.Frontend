using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Unisuper.GestorCorrespondencia.Frontend.Model
{
    public class Conexion
    {
        public int idsucursal { get; set; }
        public string nombre { get; set; }
        public string direccionip { get; set; }
        public int puerto { get; set; }
    }

    public class Tienda
    {
        public int idsucursal { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public Conexion conexion { get; set; }
    }

    public class TiendaData
    {
        public int from { get; set; }
        public int to { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int current_page { get; set; }
        public object prev_page { get; set; }
        public int next_page { get; set; }
        public int last_page { get; set; }
        public List<Tienda> data { get; set; }
        public int idsucursal { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public Conexion conexion { get; set; }
    }
}
