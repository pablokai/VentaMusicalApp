using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaMusical.Model;
using VentaMusical.DataAccess.Interface;
using System.Data;


namespace VentaMusical.DataAccess
{
    public class CancionDA 
    {
        private readonly ConnectionManager connectionManager;

        public CancionDA ()
        {
            connectionManager = new ConnectionManager ();
        }

        public async Task<List<Cancion>> ListarCancion()
        {
            List<Cancion> list = new List<Cancion>();
            try
            {
                var connection = connectionManager.GetConnection ();
                var result = await connection.QueryAsync<Cancion>(
                    sql: "usp_ListarCanciones",
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

        public async Task<Respuesta<CancionAgregar>> InsertarCancion(Cancion cancion)
        {
            Respuesta<CancionAgregar> respuesta = new Respuesta<CancionAgregar>();
            try
            {
                var connection = connectionManager.GetConnection();
                var parameters = new DynamicParameters();

                parameters.Add("@CodigoGenero", cancion.CodigoGenero, System.Data.DbType.Int32);
                parameters.Add("@NombreCancion", cancion.NombreCancion, System.Data.DbType.String);
                parameters.Add("@Precio", cancion.Precio, System.Data.DbType.Decimal);
                parameters.Add("@Portada", cancion.Portada, DbType.String);

                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);

                var result = await connection.QueryAsync<Respuesta<CancionAgregar>>(
                    sql: "usp_InsertarCancion",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                respuesta = result.FirstOrDefault();

                if (respuesta.Estado == 0)
                {
                    respuesta.Lista.Add(new CancionAgregar
                    {
                        CodigoGenero = cancion.CodigoGenero,
                        NombreCancion = cancion.NombreCancion,
                        Precio = cancion.Precio,
                    });
                }

            }
            catch (Exception)
            {
                throw;
            }

            return respuesta;
        }

        public async Task<Respuesta<CancionEditar>> ModificarCancion(Cancion cancion)
        {
            Respuesta<CancionEditar> respuesta = new Respuesta<CancionEditar>();

            try
            {
                var connection = connectionManager.GetConnection();
                var parameters = new DynamicParameters();

                parameters.Add("@CodigoCancion", cancion.CodigoCancion, System.Data.DbType.Int32);
                parameters.Add("@CodigoGenero", cancion.CodigoGenero, System.Data.DbType.Int32);
                parameters.Add("@NombreCancion", cancion.NombreCancion, System.Data.DbType.String);
                parameters.Add("@Precio", cancion.Precio, System.Data.DbType.Decimal);
                parameters.Add("@Portada", cancion.Portada, DbType.String);

                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);

                var result = await connection.QueryAsync<Respuesta<CancionEditar>>(
                    sql: "usp_ModificarCancion",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                respuesta = result.FirstOrDefault();

                if (respuesta.Estado == 0)
                {
                    respuesta.Lista.Add(new CancionEditar
                    {
                        CodigoCancion = cancion.CodigoCancion,
                        CodigoGenero = cancion.CodigoGenero,
                        NombreCancion = cancion.NombreCancion,
                        Precio = cancion.Precio
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return respuesta;
        }

        public async Task<Respuesta<CancionEditar>> ObtenerCancionPorID(Cancion cancion)
        {
            Respuesta<CancionEditar> respuesta = new Respuesta<CancionEditar>();
            try
            {
                var connection = connectionManager.GetConnection();

                var parameters = new DynamicParameters();

                parameters.Add("@CodigoCancion", cancion.CodigoCancion, System.Data.DbType.Int32);

                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType:System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);

                using( var multi = await connection.QueryMultipleAsync("usp_ObtenerCancionPorID", parameters))
                {
                    var data = await multi.ReadAsync<CancionEditar>();
                    var respuestaSp = await multi.ReadAsync<Respuesta<CancionEditar>>();

                    respuesta.Mensaje = respuestaSp.FirstOrDefault().Mensaje;
                    respuesta.Estado = respuestaSp.FirstOrDefault().Estado;

                    if(respuesta.Estado == 0)
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
