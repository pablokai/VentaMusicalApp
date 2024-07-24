using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VentaMusical.Model
{
    public class CancionDetalle
    {
        public int CodigoCancion { get; set; }
        public int CodigoGenero { get; set; }
        public string NombreCancion { get; set; }
        public decimal Precio { get; set; }
        public string Portada { get; set; }
    }
}
