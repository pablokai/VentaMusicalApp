using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VentaMusical.Model
{
    public class DatosTarjeta
    {
        [Display(Name = "Titular de la tarjeta")]
        public string TitularTarjeta { get; set; }

        [Display(Name = "Número de tarjeta")]
        public string NumeroTarjeta { get; set; }

        [Display(Name = "Tipo de tarjeta")]
        public int TipoTarjeta { get; set; }

        [Display(Name = "Expiración")]
        public DateTime FechaExpiracion { get; set; }

        [Display(Name = "CVV")]
        public int CVV { get; set; }

    }
}
