using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaMusical.Model
{
    public class Respuesta<T>
    {
        public int? Estado { get; set; }

        public string Mensaje { get; set; }

        public List<T> Lista { get; set; }

        public Respuesta()
        {
            this.Lista = new List<T>();
        }
    }
}
