using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaMusical.DataAccess;
using VentaMusical.DataAccess.Interface;
using VentaMusical.Model;
namespace VentaMusical.BusinessLogic
{
    public class VentaBL
    {

        private readonly VentaDA ventaDA;

        public VentaBL()
        {
            this.ventaDA = new VentaDA();
        }


        public async Task<List<Cancion>> ListarCanciones()
        {
            return await ventaDA.ListarCancion();
        }

        public async Task<Respuesta<Venta>> InsertarVenta(Venta venta)
        {
            return await ventaDA.InsertarVenta(venta);
        }

    }
}
