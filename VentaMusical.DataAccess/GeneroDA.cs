using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaMusical.DataAccess.Interface;
using VentaMusical.Model;

namespace VentaMusical.DataAccess
{
    public class GeneroDA 
    {

        private readonly ConnectionManager connectionManager;
        public GeneroDA() {
            connectionManager = new ConnectionManager();
        }
        public async Task<List<Genero>> ListarGeneros()
        {
            List<Genero> list = new List<Genero>();
            try
            {
                var connection  = connectionManager.GetConnection();

                var result = await connection.QueryAsync<Genero>(
                    sql: "usp_ListarGeneros",
                    commandType: System.Data.CommandType.StoredProcedure
                );

                if( result != null )
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
        public async Task<Respuesta<GeneroAgregar>> InsertarGenero(Genero genero)
        {
            Respuesta<GeneroAgregar> respuesta = new Respuesta<GeneroAgregar>();
            try
            {
                var connection = connectionManager.GetConnection();

                var parameters = new DynamicParameters();

                parameters.Add("@CodigoGenero", genero.CodigoGenero, System.Data.DbType.Int32);
                parameters.Add("@Descripcion", genero.Descripcion, System.Data.DbType.String);

                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);

                var result = await connection.QueryAsync<Respuesta<GeneroAgregar>>(
                    sql: "usp_InsertarGenero",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                respuesta = result.FirstOrDefault();

                if (respuesta.Estado == 0)
                {
                    respuesta.Lista.Add(new GeneroAgregar
                    {
                        Descripcion = genero.Descripcion,
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }

            return respuesta;
        }

        public async Task<Respuesta<GeneroEditar>> ModificarGenero(Genero genero)
        {

            Respuesta<GeneroEditar> respuesta = new Respuesta<GeneroEditar>();
            try
            {
                var connection = connectionManager.GetConnection();

                var parameters = new DynamicParameters();

                parameters.Add("@CodigoGenero", genero.CodigoGenero, System.Data.DbType.Int32);
                parameters.Add("@Descripcion", genero.Descripcion, System.Data.DbType.String);

                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);

                var result = await connection.QueryAsync<Respuesta<GeneroEditar>>(
                    sql: "usp_ModificarGenero",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                respuesta = result.FirstOrDefault();

                if (respuesta.Estado == 0)
                {
                    respuesta.Lista.Add(new GeneroEditar
                    {
                        CodigoGenero = genero.CodigoGenero,
                        Descripcion = genero.Descripcion,
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }

            return respuesta;
        }

        public async Task<Respuesta<Genero>> EliminarGenero(Genero genero)
        {
            Respuesta<Genero> respuesta = new Respuesta<Genero>();
            try
            {
                var connection = connectionManager.GetConnection();

                var parameters = new DynamicParameters();

                parameters.Add("@CodigoGenero", genero.CodigoGenero, System.Data.DbType.Int32);

                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 200);

                var result = await connection.QueryAsync<Respuesta<Genero>>(
                    sql: "usp_EliminarGenero",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                respuesta = result.FirstOrDefault();

            }
            catch (Exception)
            {

                throw;
            }

            return respuesta;
        }

        public async Task<Respuesta<GeneroEditar>> ObtenerGeneroPorID(Genero genero)
        {
            Respuesta<GeneroEditar> respuesta = new Respuesta<GeneroEditar>();
            try
            {
                var connection = connectionManager.GetConnection();

                var parameters = new DynamicParameters();

                parameters.Add("@CodigoGenero", genero.CodigoGenero, System.Data.DbType.Int32);

                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);

                using (var multi = await connection.QueryMultipleAsync("usp_ObtenerGeneroPorID", parameters))
                {
                    var data = await multi.ReadAsync<GeneroEditar>();
                    var respuestaSp = await multi.ReadAsync<Respuesta<GeneroEditar>>();

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
