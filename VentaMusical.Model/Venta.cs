using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaMusical.Model
{
    public class Venta
    {
        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public string IdUsuario { get; set; }
        public int NumeroFactura { get; set; }
        public int CodigoCancion { get; set; }
        public List<int> IdCanciones { get; set; }
        public string NombreCancion { get; set; }
        public decimal Precio { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
       


    }
}
