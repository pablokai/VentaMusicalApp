using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaMusical.Model;

namespace VentaMusical.DataAccess.Interface
{
    public interface  ICancionDA
    {
        Task<List<Cancion>> ListarCancion();

        Task<Respuesta<CancionAgregar>> InsertarCancion(Cancion cancion);
        Task<Respuesta<CancionEditar>> ModificarCancion(Cancion cancion);
        Task<Respuesta<CancionEditar>> ObtenerCancionPorID(Cancion cancion);
    }
}
