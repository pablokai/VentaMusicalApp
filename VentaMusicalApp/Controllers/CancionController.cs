using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VentaMusical.BusinessLogic;
using VentaMusical.BusinessLogic.Extensions;
using VentaMusical.Model;

namespace VentaMusicalApp.Controllers
{
    public class CancionController : Controller
    {
        private readonly CancionBL cancionBL;
        private readonly GeneroBL generoBL;
        private readonly Base64Converter base64Converter = new Base64Converter();
        public CancionController()
        {
            cancionBL = new CancionBL();
            generoBL = new GeneroBL(); 
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                var canciones = await cancionBL.ListarCanciones();
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

        public async Task<ActionResult> Details()
        {
            
            return View();
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.Generos = await ObtenerGeneros();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create( CancionAgregar cancion)
        {
            try
            {
                ViewBag.Generos = await ObtenerGeneros();
                if (!ModelState.IsValid)
                {
                    return View(cancion);
                }

                var conversion = base64Converter.ConvertIFormFileToBase64String(cancion.Archivo);
                var base64 = "data:image/jpeg;base64," + conversion;
                Cancion nuevaCancion = new Cancion()
                {
                    CodigoGenero = cancion.CodigoGenero,
                    NombreCancion = cancion.NombreCancion,
                    Precio = cancion.Precio,
                    Portada = base64,
                };
                var respuesta = await cancionBL.InsertarCancion(nuevaCancion);

                ViewBag.ValorMensaje = respuesta.Estado;
                ViewBag.MensajeProceso = respuesta.Mensaje;

                return View(respuesta.Lista.FirstOrDefault());
            }
            catch (Exception ex)
            {
                ViewBag.ValorMensaje = 1;
                ViewBag.MensajeProceso = "Error al crear la canción" + ex.Message;
                return View(cancion);
            }
        }

        public async Task<List<SelectListItem>> ObtenerGeneros()
        {
            List<Genero> respuesta = await generoBL.ListarGeneros();

            if (respuesta != null)
            {
                List<SelectListItem> dropdown = respuesta.Select(x => new SelectListItem
                {
                    Text = x.Descripcion,
                    Value = x.CodigoGenero.ToString(),
                }).ToList();

                return dropdown;
            }

            return new List<SelectListItem>();
          
            
        }

        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                ViewBag.Generos = await ObtenerGeneros();
                CancionEditar cancion = new CancionEditar();
                Cancion nuevaCancion = new Cancion()
                {
                    CodigoCancion = id,
                };
                var respuesta = await cancionBL.ObtenerCancionPorID(nuevaCancion);
                return View(respuesta.Lista.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit( CancionEditar cancion)
        {
            try
            {
                ViewBag.Generos = await ObtenerGeneros();

                if (!ModelState.IsValid)
                {
                    return View(cancion);
                }

                var conversion = base64Converter.ConvertIFormFileToBase64String(cancion.Archivo);
                var base64 = "data:image/jpeg;base64," + conversion;
                Cancion nuevaCancion = new Cancion()
                {
                    CodigoCancion = cancion.CodigoCancion,
                    CodigoGenero = cancion.CodigoGenero,
                    NombreCancion = cancion.NombreCancion,
                    Precio = cancion.Precio,
                    Portada = base64,
                };
                var respuesta = await cancionBL.ModificarCancion(nuevaCancion);

                ViewBag.ValorMensaje = respuesta.Estado;
                ViewBag.MensajeProceso = respuesta.Mensaje;

                return View(respuesta.Lista.FirstOrDefault());
            }
            catch (Exception ex) 
            {
                ViewBag.ValorMensaje = 1;
                ViewBag.MensajeProceso = "Error al editar la canción" + ex.Message;
                return View(cancion);
            }

           
        }
    }
}
