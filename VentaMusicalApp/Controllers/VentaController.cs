using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VentaMusical.BusinessLogic;
using VentaMusical.BusinessLogic.Extensions;
using VentaMusical.Model;


namespace VentaMusicalApp.Controllers
{
    public class VentaController : Controller
    {
       
        
        private readonly VentaBL ventaBL;
        private readonly Base64Converter base64Converter = new Base64Converter();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public VentaController()
        {
            ventaBL = new VentaBL();
           

        }



        // GET: Venta
        public async Task<System.Web.Mvc.ActionResult> Venta()
        {
            try
            {
                var canciones = await ventaBL.ListarCanciones();
                if (canciones != null)
                {
                    return View(canciones);
                }
                else
                {
                    return View(new List<Cancion>());
                }
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message });
            }
        }

        public   System.Web.Mvc.ActionResult Factura()
        {
            var UserId = User.Identity.GetUserId();
            var user = UserManager.FindById(UserId);
            Respuesta<Venta> venta =  Session["sesionRespuesta"] as Respuesta<Venta>;

            ViewBag.NombreUsuario = user.PrimerNombre + ' ' + user.PrimerApellido + ' ' + user.SegundoApellido;
            ViewBag.Identificacion = user.NumeroIdentificacion;
            ViewBag.FechaFactura = venta.Lista.First().FechaCompra;
            ViewBag.NumFac = venta.Lista.First().NumeroFactura;
            ViewBag.SubTotal = venta.Lista.First().SubTotal;
            ViewBag.Total = venta.Lista.First().Total;


            return View(venta.Lista);
        }


        [System.Web.Mvc.HttpPost]
        public async Task<System.Web.Mvc.JsonResult> Venta([FromBody] FacturaRequest request)
        {
            try
            {
                var UserId = User.Identity.GetUserId();
                var user = UserManager.FindById(UserId);

                Venta nuevaVenta = new Venta()
                {
                    FechaCompra = request.Fecha,
                    IdUsuario = user.Id,
                    IdCanciones = request.Records,
                    SubTotal = request.Subtotal,
                    Total = request.Total
                };
                 var respuesta  = await ventaBL.InsertarVenta(nuevaVenta);
                Session["sesionRespuesta"] = respuesta;

                bool success = true; 
                string redirectUrl = Url.Action("Factura", "Venta");
                return Json(new { success = success, redirectUrl = redirectUrl });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrio un error al procesar la factura: " + ex.Message;
                return Json(new { });  
            }
        }
    }
}
