using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaMusical.Model;

namespace VentaMusical.DataAccess.Interface
{
    public interface ICompraDA
    {
        Task<List<Compra>> ListarFacturas();

        Task<Respuesta<CompraAgregar>> InsertarCompra(Compra compra);

    }
}
