using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VentaMusical.Model
{
    public class GeneroEditar
    {

        [Display(Name = "Código Género")]
        public int CodigoGenero { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
