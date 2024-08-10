using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaMusical.Model
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public int UsuarioId { get; set; }
        public string NumeroFactura { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public List<CompraDetalle> DetallesCompra { get; set; } = new List<CompraDetalle> { };

    }
}
