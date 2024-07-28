using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaMusical.DataAccess.Interface;
using VentaMusical.Model;

namespace VentaMusical.DataAccess
{
    public class UsuarioDA 
    {
        private readonly ConnectionManager connectionManager;

        public UsuarioDA()
        {
            connectionManager = new ConnectionManager();
        }

        public async Task<List<TipoTarjeta>> ListarTipoTarjeta()
        {
            List<TipoTarjeta> list = new List<TipoTarjeta>();
            try
            {
                var connection = connectionManager.GetConnection();
                var result = await connection.QueryAsync<TipoTarjeta>(
                    sql: "usp_ListarTipoTarjeta",
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result != null)
                {
                    list = result.ToList();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            List<Usuario> list = new List<Usuario>();
            try
            {
                var connection = connectionManager.GetConnection();
                var result = await connection.QueryAsync<Usuario>(
                    sql: "usp_ListarUsuarios",
                    commandType: System.Data.CommandType.StoredProcedure);

                if (result != null)
                {
                    list = result.ToList();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public async Task<Respuesta<Usuario>> ModificarUsuario(Usuario usuario)
        {
            Respuesta<Usuario> respuesta = new Respuesta<Usuario>();

            try
            {
                var connection = connectionManager.GetConnection();
                var parameters = new DynamicParameters();

                parameters.Add("@Id", usuario.Id, System.Data.DbType.String);
                parameters.Add("@Email", usuario.Email, System.Data.DbType.String);
                parameters.Add("@NumeroIdentificacion", usuario.NumeroIdentificacion, System.Data.DbType.String);
                parameters.Add("@PrimerNombre", usuario.PrimerNombre, System.Data.DbType.String);
                parameters.Add("@SegundoNombre", usuario.SegundoNombre, System.Data.DbType.String);
                parameters.Add("@PrimerApelllido", usuario.PrimerApellido, System.Data.DbType.String);
                parameters.Add("@SegundoApelllido", usuario.SegundoApellido, System.Data.DbType.String);
                parameters.Add("@Genero", usuario.Genero, System.Data.DbType.String);
                parameters.Add("@NumeroTarjeta", usuario.NumeroTarjeta, System.Data.DbType.String);
                parameters.Add("@IdTipoTarjeta", usuario.IdTipoTarjeta, System.Data.DbType.Int32);

                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);

                var result = await connection.QueryAsync<Respuesta<Usuario>>(
                    sql: "usp_ModificarUsuario",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                respuesta = result.FirstOrDefault();

                if (respuesta.Estado == 0)
                {
                    respuesta.Lista.Add(new Usuario
                    {
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return respuesta;
        }

        public async Task<Respuesta<Usuario>> ObtenerUsuarioPorID(Usuario usuario)
        {
            Respuesta<Usuario> respuesta = new Respuesta<Usuario>();
            try
            {
                var connection = connectionManager.GetConnection();

                var parameters = new DynamicParameters();

                parameters.Add("@Id", usuario.Id, System.Data.DbType.String);

                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);

                using (var multi = await connection.QueryMultipleAsync("usp_ListarUsuarioPorID", parameters))
                {
                    var data = await multi.ReadAsync<Usuario>();
                    var respuestaSp = await multi.ReadAsync<Respuesta<Usuario>>();

                    respuesta.Mensaje = respuestaSp.FirstOrDefault().Mensaje;
                    respuesta.Estado = respuestaSp.FirstOrDefault().Estado;

                    if (respuesta.Estado == 0)
                    {
                        respuesta.Lista = data.ToList();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return respuesta;
        }

    }
}
