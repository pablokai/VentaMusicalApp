using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaMusical.Model;

namespace VentaMusical.DataAccess.Interface
{
    public interface IGeneroDA
    {
        Task<List<Genero>> ListarGeneros();

        Task<Respuesta<GeneroAgregar>> InsertarGenero(Genero genero);

        Task<Respuesta<GeneroEditar>> ModificarGenero(Genero genero);

        Task<Respuesta<Genero>> EliminarGenero(Genero genero);

        Task<Respuesta<GeneroEditar>> ObtenerGeneroPorID(Genero genero);

    }
}
