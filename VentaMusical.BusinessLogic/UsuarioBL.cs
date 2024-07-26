using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaMusical.DataAccess;
using VentaMusical.Model;

namespace VentaMusical.BusinessLogic
{
    public class UsuarioBL
    {
        private readonly UsuarioDA usuarioDA;

        public UsuarioBL()
        {
            this.usuarioDA = new UsuarioDA();
        }

        public async Task<List<TipoTarjeta>> ListarTipoTarjeta()
        {
            return await usuarioDA.ListarTipoTarjeta();
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            return await usuarioDA.ListarUsuarios();
        }

        public async Task<Respuesta<Usuario>> ModificarUsuario(Usuario usuario)
        {
            return await usuarioDA.ModificarUsuario(usuario);
        }

        public async Task<Respuesta<Usuario>> ObtenerUsuarioPorID(Usuario usuario)
        {
            return await usuarioDA.ObtenerUsuarioPorID(usuario);
        }
    }
}
