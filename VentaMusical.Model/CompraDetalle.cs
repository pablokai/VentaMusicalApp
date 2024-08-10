using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaMusical.Model
{
    public class CompraDetalle
    {
        public int IdDetalle { get; set; }
        public int IdCompra {  get; set; }
        public int CodigoCancion { get; set; }

        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
