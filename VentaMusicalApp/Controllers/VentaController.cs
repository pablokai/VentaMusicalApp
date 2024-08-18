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
using VentaMusical.DataAccess;
using VentaMusical.Model;


namespace VentaMusicalApp.Controllers
{

    [Authorize(Roles = "Admin, User")]

    public class VentaController : Controller
    {
       
        
        private readonly VentaBL ventaBL;
        private readonly Base64Converter base64Converter = new Base64Converter();
        private readonly UsuarioBL usuarioBL;


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
            usuarioBL = new UsuarioBL();

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


        // GET: Venta
        public async Task<System.Web.Mvc.ActionResult> PagoTarjeta()
        {
            try
            {
                var UserId = User.Identity.GetUserId();
                var user = UserManager.FindById(UserId);
                
                var listaTarjetas = await ObtenerTipoTarjeta();

                PagoTarjeta pago = new PagoTarjeta()
                {
                    NumeroTarjeta = user.NumeroTarjeta,
                    IdTipoTarjeta = user.IdTipoTarjeta,
                    TitularTarjeta = user.PrimerNombre + ' ' + user.PrimerApellido + ' ' + user.SegundoApellido,
                    NombreTarjeta = listaTarjetas.Find(x => x.Value == user.IdTipoTarjeta.ToString()).Text
                };

                return View(pago);
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message });
            }
        }

        [System.Web.Mvc.HttpPost]
        public async Task<System.Web.Mvc.ActionResult> PagoTarjeta(PagoTarjeta pago)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(pago);
                }

                Respuesta<Venta> venta = Session["sesionRespuesta"] as Respuesta<Venta>;
                var UserId = User.Identity.GetUserId();
                var user = UserManager.FindById(UserId);

                PagoTarjeta nuevoPagoTarjeta = new PagoTarjeta()
                {
                    NumeroFactura = venta.Lista.FirstOrDefault().NumeroFactura,
                    FechaExpiracion = pago.FechaExpiracion,
                    CVC = pago.CVC,
                    IdTipoTarjeta = user.IdTipoTarjeta,
                    NumeroTarjeta = user.NumeroTarjeta,
                    IdUsuario = user.Id,

                };

                var respuesta = await ventaBL.GuardarDatosTarjeta(nuevoPagoTarjeta);
                if (respuesta != null)
                {
                    var porcentajeImpuestoTarjeta = venta.Lista.FirstOrDefault().SubTotal * 0.02m;
                    venta.Lista.FirstOrDefault().Total = venta.Lista.FirstOrDefault().Total + porcentajeImpuestoTarjeta;
                    return RedirectToAction("Factura");
                }
                else
                {
                    return RedirectToAction("Venta");
                }
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message });
            }
        }

        [System.Web.Mvc.HttpPost]
        public async Task<System.Web.Mvc.ActionResult> PagoEfectivo()
        {
            try
            {
                return RedirectToAction("Factura");

            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message });
            }
        }

        public System.Web.Mvc.ActionResult Factura()
        {
            var UserId = User.Identity.GetUserId();
            var user = UserManager.FindById(UserId);
            Respuesta<Venta> venta = Session["sesionRespuesta"] as Respuesta<Venta>;

            ViewBag.NombreUsuario = user.PrimerNombre + ' ' + user.PrimerApellido + ' ' + user.SegundoApellido;
            ViewBag.Identificacion = user.NumeroIdentificacion;
            ViewBag.FechaFactura = venta.Lista.First().FechaCompra.ToString("d");
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
                string redirectUrl = Url.Action("PagoTarjeta", "Venta");
                return Json(new { success = success, redirectUrl = redirectUrl });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ocurrio un error al procesar la factura: " + ex.Message;
                return Json(new { });  
            }
        }

        public async Task<List<SelectListItem>> ObtenerTipoTarjeta()
        {
            List<TipoTarjeta> respuesta = await usuarioBL.ListarTipoTarjeta();

            if (respuesta != null)
            {
                List<SelectListItem> dropdown = respuesta.Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.IdTipoTarjeta.ToString(),
                }).ToList();

                return dropdown;
            }
            return new List<SelectListItem>();
        }
    }
}
