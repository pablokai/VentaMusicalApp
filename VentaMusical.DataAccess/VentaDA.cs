using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentaMusical.Model;
namespace VentaMusical.DataAccess
{
    public class VentaDA
    {


        private readonly ConnectionManager connectionManager;


        public VentaDA()
        {
            connectionManager = new ConnectionManager();
        }



        public async Task<List<Cancion>> ListarCancion()
        {
            List<Cancion> list = new List<Cancion>();
            try
            {
                var connection = connectionManager.GetConnection();
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

        public async Task<Respuesta<Venta>> InsertarVenta(Venta venta)
        {
            Respuesta<Venta> respuesta = new Respuesta<Venta>();
            try
            {
                var connection = connectionManager.GetConnection();
                var parameters = new DynamicParameters();
                string idsCanciones = string.Join(",", venta.IdCanciones);

                parameters.Add("@idUsuario", venta.IdUsuario, System.Data.DbType.String);
                parameters.Add("@idCanciones", idsCanciones, System.Data.DbType.String);
                parameters.Add("@Total", venta.Total, System.Data.DbType.Decimal);
                parameters.Add("@Subtotal", venta.SubTotal, System.Data.DbType.Decimal);

                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);

                var result = await connection.QueryAsync<Venta>(
                    sql: "usp_InsertarVenta",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                    );

                foreach (var x  in result)
                {
                    
                        respuesta.Lista.Add(new Venta
                        {
                            FechaCompra = x.FechaCompra,
                            IdUsuario = x.IdUsuario,
                            NumeroFactura = x.NumeroFactura,
                            CodigoCancion = x.CodigoCancion,
                            NombreCancion = x.NombreCancion,
                            Precio = x.Precio,
                            SubTotal = x.SubTotal,
                            Total = x.Total
                        });
                    
                }

              

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return respuesta;
        }

        public async Task<Respuesta<PagoTarjeta>> GuardarDatosTarjeta(PagoTarjeta pago)
        {
            Respuesta<PagoTarjeta> respuesta = new Respuesta<PagoTarjeta>();
            try
            {
                var connection = connectionManager.GetConnection();

                var parameters = new DynamicParameters();

                parameters.Add("@NumeroTarjeta", pago.NumeroTarjeta, System.Data.DbType.String);
                parameters.Add("@FechaExpiracion", pago.FechaExpiracion, System.Data.DbType.DateTime);
                parameters.Add("@CVC", pago.CVC, System.Data.DbType.Int32);
                parameters.Add("@IdUsuario", pago.IdUsuario, System.Data.DbType.String);
                parameters.Add("@IdTipoTarjeta", pago.IdTipoTarjeta, System.Data.DbType.Int32);
                parameters.Add("@NumFactura", pago.NumeroFactura, System.Data.DbType.Decimal);


                parameters.Add("@Estado", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@Mensaje", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Output, size: 250);


                var result = await connection.QueryAsync<Respuesta<PagoTarjeta>>(
                    sql: "usp_GuardarDatosTarjeta",
                    param: parameters,
                    commandType: System.Data.CommandType.StoredProcedure);

                respuesta = result.FirstOrDefault();



            }
            catch (Exception)
            {
                throw;
            }

            return respuesta;
        }

    }
}
