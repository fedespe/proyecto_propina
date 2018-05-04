using BL;
using ET;
using PropinaWeb.ViewModel.ColectaViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropinaWeb.Controllers
{
    public class ColectaController : Controller
    {
        private ColectaBL colectaBL = new ColectaBL();

        public ActionResult ListaColecta()
        {
            if (Session["TipoUsuario"] != null)// && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    return View(colectaBL.obtenerTodos().OrderByDescending(p => p.Fecha).ToList());
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
        //GET: Colecta/AltaColecta
        public ActionResult AltaColecta()
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    return View(new AltaColectaViewModel());
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

        //POST: Colecta/AltaColecta
        [HttpPost]
        public ActionResult AltaColecta(AltaColectaViewModel crearVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    crearVM.completarPlanilla();
                    colectaBL.altaColecta(crearVM.colecta);
                    return RedirectToAction("ListaColecta");
                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
            else {
                return View(crearVM);
            }
        }
        //GET: Planilla/Ver
        public ActionResult Ver(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("EMPLEADO") || Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        AltaColectaViewModel viewModel = new AltaColectaViewModel();
                        viewModel.colecta = colectaBL.obtener(id);
                        EmpleadoBL emp = new EmpleadoBL();
                        viewModel.EmpleadoColecta.Empleado = emp.obtener(Convert.ToInt32(Session["IdUsuario"]));
                        viewModel.colecta.EmpleadoColecta.Empleado = emp.obtener(viewModel.colecta.EmpleadoColecta.Empleado.Id);
                        //viewModel.CantidadEmpleados88 = emp.obtenerCantidadEmpleados88();
                        //viewModel.PorcentajeFirmas = viewModel.planilla.Empleados.Count * 100 / (viewModel.CantidadEmpleados88 - 1);
                        if (Session["TipoUsuario"] != null && Session["TipoUsuario"].ToString().Equals("EMPLEADO"))
                        {
                            viewModel.comprobarFirmas();
                        }
                        return View(viewModel);
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

        //GET: Colecta/Colaborar
        public ActionResult Colaborar(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("EMPLEADO")))
            {
                try
                {
                    if (id != 0)
                    {
                        ColaborarViewModel viewModel = new ColaborarViewModel();
                        //viewModel.IdColecta = id;
                        viewModel.Colecta.Id = id;
                        return View(viewModel);
                    }
                    else {
                        ViewBag.Mensaje = "No selecciono la colecta correctamente.";
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
                    return RedirectToAction("Index", "Home");
                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
        }

        //POST: Colecta/AltaColecta
        [HttpPost]
        public ActionResult Colaborar(ColaborarViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //viewModel.completarPlanilla();
                    viewModel.Colecta.EmpleadoColecta.MontoDolares = viewModel.MontoDolares;
                    viewModel.Colecta.EmpleadoColecta.MontoPesos = viewModel.MontoPesos;
                    viewModel.Colecta.EmpleadoColecta.Empleado = new Empleado();
                    viewModel.Colecta.EmpleadoColecta.Empleado.Id = Convert.ToInt32(Session["IdUsuario"]);
                    colectaBL.Colaborar(viewModel.Colecta);
                    return RedirectToAction("Ver", "Colecta", new { id = viewModel.Colecta.Id });
                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
            else {
                return View(viewModel);
            }
        }

        //GET: Colecta/QuitarColaboracion
        public ActionResult QuitarColaboracion(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("EMPLEADO")))
            {
                try
                {
                    if (id != 0)
                    {
                        colectaBL.quitarColaboracion(id, Convert.ToInt32(Session["IdUsuario"]));
                        ViewBag.Mensaje = "Se quitó la firma con éxito";
                        return RedirectToAction("Ver", "Colecta", new { id = id });
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

        //GET: Colecta/HabilitarColecta
        public ActionResult HabilitarColecta(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        colectaBL.habilitarColecta(id);
                        return RedirectToAction("ListaColecta");
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

        //GET: Colecta/DeshabilitarColecta
        public ActionResult DeshabilitarColecta(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        colectaBL.deshabilitarColecta(id);
                        return RedirectToAction("ListaColecta");
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

        //GET: Colecta/EliminarColecta
        public ActionResult EliminarColecta(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        colectaBL.eliminarColecta(id);
                        return RedirectToAction("ListaColecta");
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




    }
}