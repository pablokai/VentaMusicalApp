using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaMusical.Model
{
    public class FacturaRequest
    {
        public List<int> Records { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
