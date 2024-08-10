using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaMusical.Model
{
    public class CompraAgregar
    {

        [Display(Name = "Id de compra")]
        public int IdCompra { get; set; }

        [Display(Name = "Fechas de compra")]
        public DateTime FechaCompra { get; set; }

        [Display(Name = "Id de Usuario")]
        public int UsuarioId { get; set; }

        [Display(Name = "Numero de Factura")]
        public string NumeroFactura { get; set; }

        [Display(Name = "SubTotal")]
        public decimal Subtotal { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "FechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "FechaModificacion")]

        public DateTime FechaModificacion { get; set; }


        [Display(Name = "Lista De los detalles de la compra")]

        public List<CompraDetalle> DetallesCompra { get; set; } = new List<CompraDetalle> { };
    }
}
