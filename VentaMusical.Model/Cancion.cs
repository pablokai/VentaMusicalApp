using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaMusical.Model
{
    public class Cancion
    {
        public int CodigoCancion { get; set; }
        public int CodigoGenero { get; set; }
        public string NombreCancion { get; set; }
        public decimal Precio { get; set; }
        public string NombreGenero { get; set; }
        public string Portada { get; set; }


    }
}
