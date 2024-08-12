using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VentaMusical.BusinessLogic;
using VentaMusical.Model;
using VentaMusicalApp.Models;

namespace VentaMusicalApp.Controllers
{
    [Authorize(Roles = "Admin, User")]

    public class UsuarioController : Controller
    {
        private readonly UsuarioBL usuarioBL;
        private ApplicationUserManager _userManager;
        public UsuarioController()
        {
            usuarioBL = new UsuarioBL();
        }

        public UsuarioController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var usuarios = await usuarioBL.ListarUsuarios();
                if (usuarios != null)
                {
                    return View(usuarios);
                }
                else
                {
                    return View(new List<Usuario>());
                }
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message, JsonRequestBehavior.AllowGet });
            }

        }

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

        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                ViewBag.TipoTarjeta = await ObtenerTipoTarjeta();
                ViewBag.Generos = await ObtenerGenerosUsuarios();
                Usuario nuevousuario = new Usuario()
                {
                    Id = id,
                };
                var respuesta = await usuarioBL.ObtenerUsuarioPorID(nuevousuario);
                var model = respuesta.Lista.FirstOrDefault();
                ChangePasswordViewModel usuario = new ChangePasswordViewModel()
                {
                    NumeroIdentificacion = model.NumeroIdentificacion,
                    Email = model.Email,
                    PrimerNombre = model.PrimerNombre,
                    SegundoNombre = model.SegundoNombre,
                    PrimerApellido = model.PrimerApellido,
                    SegundoApellido = model.SegundoApellido,
                    Genero = model.Genero,
                    IdTipoTarjeta = (int)model.IdTipoTarjeta,
                    NumeroTarjeta = model.NumeroTarjeta
                };

                return View(usuario);
            }
            catch (Exception ex)
            {
                return Json(new { mensaje = ex.Message });
            }
        }

        
        [HttpPost]
        public async Task<ActionResult> Edit(ChangePasswordViewModel model)
        {
            try
            {
                ViewBag.TipoTarjeta = await ObtenerTipoTarjeta();
                ViewBag.Generos = await ObtenerGenerosUsuarios();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.Password);
                if (!result.Succeeded)
                {
                    ViewBag.ValorMensaje = 1;
                    ViewBag.MensajeProceso = "Error al editar el usuario " + result.Errors.FirstOrDefault();
                    return View(model);

                }
                Usuario nuevoUsuario = new Usuario()
                {
                    Id = model.Id,
                    NumeroIdentificacion = model.NumeroIdentificacion,
                    Email = model.Email,
                    PrimerNombre = model.PrimerNombre,
                    SegundoNombre = model.SegundoNombre,
                    PrimerApellido = model.PrimerApellido,
                    SegundoApellido = model.SegundoApellido,
                    Genero = model.Genero,
                    IdTipoTarjeta= model.IdTipoTarjeta,
                    NumeroTarjeta = model.NumeroTarjeta
                };
                var respuesta = await usuarioBL.ModificarUsuario(nuevoUsuario);
                if (respuesta.Estado == 1)
                {
                    ViewBag.ValorMensaje = 1;
                    ViewBag.MensajeProceso = "Error al editar el usuario " + respuesta.Mensaje;
                    return View(model);
                }

                ViewBag.ValorMensaje = respuesta.Estado;
                ViewBag.MensajeProceso = respuesta.Mensaje;

                ChangePasswordViewModel usuario = new ChangePasswordViewModel()
                {
                    NumeroIdentificacion = model.NumeroIdentificacion,
                    Email = model.Email,
                    PrimerNombre = model.PrimerNombre,
                    SegundoNombre = model.SegundoNombre,
                    PrimerApellido = model.PrimerApellido,
                    SegundoApellido = model.SegundoApellido,
                    Genero = model.Genero,
                    IdTipoTarjeta = (int)model.IdTipoTarjeta,
                    NumeroTarjeta = model.NumeroTarjeta
                };

                return View(usuario);
            }
            catch (Exception ex)
            {
                ViewBag.ValorMensaje = 1;
                ViewBag.MensajeProceso = "Error al editar el usuario " + ex.Message;
                return View(model);
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

        public async Task<List<SelectListItem>> ObtenerGenerosUsuarios()
        {
            List<SelectListItem> dropdown = new List<SelectListItem>(){
                new SelectListItem
                {
                    Text = "Masculino",
                    Value =  "Masculino",
                },
                 new SelectListItem
                 {
                     Text = "Femenino",
                     Value =  "Femenino",
                 }
                };
            return dropdown;

        }
    }
}