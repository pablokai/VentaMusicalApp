using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaMusical.Model
{
    public class PagoTarjeta
    {
        [Display(Name = "Número de tarjeta")]
        public string NumeroTarjeta { get; set; }
        
        [Display(Name = "Fecha de expiración")]
        public DateTime? FechaExpiracion { get; set; }

        [Display(Name = "CVC")]
        public int? CVC { get; set; }

        [Display(Name = "Usuario")]
        public string IdUsuario { get; set; }

        [Display(Name = "Id tarjeta")]
        public int IdTipoTarjeta { get; set; }

        [Display(Name = "Tipo de tarjeta")]
        public string NombreTarjeta { get; set; }

        [Display(Name = "Numero de factura")]
        public int? NumeroFactura { get; set; }

        [Display(Name = "Titular de la tarjeta")]
        public string TitularTarjeta { get; set; }

    }
}
