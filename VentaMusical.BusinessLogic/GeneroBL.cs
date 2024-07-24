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
    public class GeneroBL
    {
        private readonly GeneroDA generoDA;
        public GeneroBL() {
            this.generoDA = new GeneroDA();
        }

        public async Task<List<Genero>> ListarGeneros()
        {
            return await generoDA.ListarGeneros();
        }

        public async Task<Respuesta<GeneroAgregar>> InsertarGenero(Genero genero)
        {
            return await generoDA.InsertarGenero(genero);
        }

        public async Task<Respuesta<GeneroEditar>> ModificarGenero(Genero genero)
        {
            return await generoDA.ModificarGenero(genero);
        }

        public async Task<Respuesta<Genero>> EliminarGenero(Genero genero)
        {
            return await generoDA.EliminarGenero(genero);
        }

        public async Task<Respuesta<GeneroEditar>> ObtenerGeneroPorID(Genero genero)
        {
            return await generoDA.ObtenerGeneroPorID(genero);
        }
    }
}
