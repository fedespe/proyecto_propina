using BL;
using ET;
using PropinaWeb.ViewModel.HomeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropinaWeb.Controllers
{
    public class HomeController : Controller
    {
        private MensajePrincipalBL mensajeBL = new MensajePrincipalBL();
        public ActionResult Index()
        {
            return View(new HomeViewModel());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //GET: Home/EliminarMensaje
        public ActionResult EliminarMensaje(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        mensajeBL.eliminarMensaje(id);
                        return RedirectToAction("Index");
                    }
                    else {
                        ViewBag.Mensaje = "No selecciono el usuario correctamente.";
                        return View("~/Views/Shared/_Mensajes.cshtml");
                    }
                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
            else
            {
                try
                {
                    ViewBag.Mensaje = "No tiene permisos para relalizar esta acción.";
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
        }

        //GET: hOME/AltaMensaje
        public ActionResult AltaMensaje()
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    return View(new MensajePrincipal());
                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
            else
            {
                try
                {
                    return RedirectToAction("Index", "Home");
                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
        }

        //POST: Home/AltaReparto
        [HttpPost]
        public ActionResult AltaMensaje(MensajePrincipal mensaje)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //reparto.completarPlanilla();
                    mensajeBL.altaMensaje(mensaje);
                    return RedirectToAction("Index", "Home");
                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
            else {
                return View(mensaje);
            }
        }

    }
}