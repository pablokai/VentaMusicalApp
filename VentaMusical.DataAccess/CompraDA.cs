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
     public class CompraDA
    {
        private readonly ConnectionManager connectionManager;

        public CompraDA()
        {
            connectionManager = new ConnectionManager();
        }

        //recibir ID
        public async Task<List<Compra>> ListarFacturas()
        {
            List<Compra> list = new List<Compra>();
            try
            {
                var connection = connectionManager.GetConnection();
                var result = await connection.QueryAsync<Compra>(
                    sql: "usp_ListarFacturas",
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

        

        public async Task<Respuesta<CompraAgregar>> InsertarCompra(Compra compra)
        {
            Respuesta<CompraAgregar> respuesta = new Respuesta<CompraAgregar>();
            try
            {

                var connection = connectionManager.GetConnection();


                var detallesCompra = new DataTable();
                detallesCompra.Columns.Add("CodigoCancion", typeof(int));
                detallesCompra.Columns.Add("Precio", typeof(decimal));
                detallesCompra.Columns.Add("Cantidad", typeof(int));

                foreach (var detalle in compra.DetallesCompra)
                {
                    detallesCompra.Rows.Add(detalle.CodigoCancion, detalle.Precio, detalle.Cantidad);
                }


                var parameters = new DynamicParameters();

                parameters.Add("@FechaCompra", compra.FechaCompra, System.Data.DbType.DateTime);
                parameters.Add("@UsuarioId", compra.UsuarioId, System.Data.DbType.Int32);
                parameters.Add("@NumeroFactura", compra.NumeroFactura, System.Data.DbType.String);
                parameters.Add("@Subtotal", compra.Subtotal, System.Data.DbType.Decimal);
                parameters.Add("@Total", compra.Total, System.Data.DbType.Decimal);
                parameters.Add("@IdCompra", dbType: DbType.Int32, direction: ParameterDirection.Output);



                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);






                var result = await connection.QueryAsync<Respuesta<CompraAgregar>>(
                    sql: "usp_InsertarGenero",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                compra.IdCompra = parameters.Get<int>("@IdCompra");


                respuesta = result.FirstOrDefault();




                if (respuesta.Estado == 0)
                {
                    respuesta.Mensaje = "Compra insertada correctamente";
                    respuesta.Lista.Add(new CompraAgregar
                    {
                        IdCompra = compra.IdCompra
                    });
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
