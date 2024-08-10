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
    public class CompraBL
    {
        private readonly CompraDA compraDA;

        public CompraBL() 
        { 
         this.compraDA = new CompraDA();
        }

        public async Task<List<Compra>> ListarFacturas()
        {
            return await compraDA.ListarFacturas();
        }

        public async Task<Respuesta<CompraAgregar>> InsertarGenero(Compra compra)
        {
            return await compraDA.InsertarCompra(compra);
        }




    }

   
}
