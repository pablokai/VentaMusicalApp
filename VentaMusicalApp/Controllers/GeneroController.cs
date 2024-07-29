using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VentaMusical.BusinessLogic;
using VentaMusical.DataAccess;
using VentaMusical.DataAccess.Interface;
using VentaMusical.Model;

namespace VentaMusicalApp.Controllers
{
    [Authorize(Roles = "Admin")]

    public class GeneroController : Controller
    {
            private readonly GeneroBL generoBL;
            public GeneroController()
            {
                generoBL = new GeneroBL();
            }


            public async Task<ActionResult> Index()
            {

                try
                {
                    var generos = await generoBL.ListarGeneros();
                    if (generos != null)
                    {
                        return View(generos);
                    }
                    else
                    {
                        return View(new List<Genero>());
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { mensaje = ex.Message });
                }
            }

            public async Task<IActionResult> Details()
            {

                return (IActionResult)View();
            }

            public ActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public async Task<ActionResult> Create(GeneroAgregar generoAgregar)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(generoAgregar);
                    }
                    Genero nuevoGenero = new Genero()
                    {
                        Descripcion = generoAgregar.Descripcion,
                    };
                    var respuesta = await generoBL.InsertarGenero(nuevoGenero);

                    ViewBag.ValorMensaje = respuesta.Estado;
                    ViewBag.MensajeProceso = respuesta.Mensaje;

                    return View(respuesta.Lista.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    ViewBag.ValorMensaje = 1;
                    ViewBag.MensajeProceso = "Error al crear el género" + ex.Message;
                    return View(generoAgregar);
                }
            }

            public async Task<ActionResult> Edit(int id)
            {
                try
                {
                    GeneroEditar cancion = new GeneroEditar();
                    Genero nuevoGenero = new Genero()
                    {
                        CodigoGenero = id,
                    };
                    var respuesta = await generoBL.ObtenerGeneroPorID(nuevoGenero);
                    return View(respuesta.Lista.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    return Json(new { mensaje = ex.Message });
                }
            }

            [HttpPost]
            public async Task<ActionResult> Edit(GeneroEditar generoEditar)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(generoEditar);
                    }
                    Genero nuevoGenero = new Genero()
                    {
                        CodigoGenero = generoEditar.CodigoGenero,
                        Descripcion = generoEditar.Descripcion,
                    };
                    var respuesta = await generoBL.ModificarGenero(nuevoGenero);

                    ViewBag.ValorMensaje = respuesta.Estado;
                    ViewBag.MensajeProceso = respuesta.Mensaje;

                    return View(respuesta.Lista.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    ViewBag.ValorMensaje = 1;
                    ViewBag.MensajeProceso = "Error al editar el género " + ex.Message;
                    return View(generoEditar);
                }
            }

            public ActionResult Delete(int id)
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Delete(int id, IFormCollection collection)
            {
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
    }
}