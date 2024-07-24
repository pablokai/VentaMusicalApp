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
    public class CancionBL
    {
        private readonly CancionDA cancionDA;

        public CancionBL()
        {
            this.cancionDA = new CancionDA();
        }

        public async Task<List<Cancion>> ListarCanciones()
        {
            return await cancionDA.ListarCancion();
        }

        public async Task<Respuesta<CancionAgregar>> InsertarCancion(Cancion cancion)
        {
            return await cancionDA.InsertarCancion(cancion);
        }

        public async Task<Respuesta<CancionEditar>> ModificarCancion(Cancion cancion)
        {
            return await cancionDA.ModificarCancion(cancion);
        }

        public async Task<Respuesta<CancionEditar>> ObtenerCancionPorID(Cancion cancion)
        {
            return await cancionDA.ObtenerCancionPorID(cancion);
        }

    }
}
