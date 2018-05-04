using BL;
using ET;
using PropinaWeb.ViewModel.PlanillaViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropinaWeb.Controllers
{
    public class PlanillaController : Controller
    {
        private PlanillaBL planillaBL = new PlanillaBL();

        public ActionResult ListaPlanillas()
        {
            if (Session["TipoUsuario"] != null)// && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    return View(planillaBL.obtenerTodos().OrderByDescending(p => p.Fecha).ToList());
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
        //GET: Planilla/AltaPlanilla
        public ActionResult AltaPlanilla()
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    return View(new AltaPlanillaViewModel());
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

        //POST: Planilla/AltaPlanilla
        [HttpPost]
        public ActionResult AltaPlanilla(AltaPlanillaViewModel crearVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    crearVM.completarPlanilla();
                    planillaBL.altaPlanilla(crearVM.planilla);
                    return RedirectToAction("ListaPlanillas");
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
                        AltaPlanillaViewModel viewModel = new AltaPlanillaViewModel();
                        viewModel.planilla = planillaBL.obtener(id);
                        EmpleadoBL emp = new EmpleadoBL();
                        viewModel.Empleado = emp.obtener(Convert.ToInt32(Session["IdUsuario"]));
                        viewModel.planilla.empleado = emp.obtener(viewModel.planilla.empleado.Id);
                        viewModel.CantidadEmpleados88 = emp.obtenerCantidadEmpleados88();
                        viewModel.PorcentajeFirmas = viewModel.planilla.Empleados.Count * 100 / (viewModel.CantidadEmpleados88 - 1);
                        if (Session["TipoUsuario"] != null && Session["TipoUsuario"].ToString().Equals("EMPLEADO")) {
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
        //GET: Planilla/FirmarPlanilla
        public ActionResult FirmarPlanilla(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("EMPLEADO")))
            {
                try
                {
                    if (id != 0)
                    {
                        planillaBL.firmarPlanilla(id, Convert.ToInt32(Session["IdUsuario"]));
                        ViewBag.Mensaje = "Firma realizada con éxito";
                        return RedirectToAction("Ver", "Planilla", new { id = id });
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
        //GET: Planilla/QuitarFirmarPlanilla
        public ActionResult QuitarFirmarPlanilla(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("EMPLEADO")))
            {
                try
                {
                    if (id != 0)
                    {
                        planillaBL.quitarFirmarPlanilla(id, Convert.ToInt32(Session["IdUsuario"]));
                        ViewBag.Mensaje = "Se quitó la firma con éxito";
                        return RedirectToAction("Ver", "Planilla", new { id = id });
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

        //GET: Planilla/HabilitarPlanilla
        public ActionResult HabilitarPlanilla(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        planillaBL.habilitarPlanilla(id);
                        return RedirectToAction("ListaPlanillas");
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

        //GET: Planilla/DeshabilitarPlanilla
        public ActionResult DeshabilitarPlanilla(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        planillaBL.deshabilitarPlanilla(id);
                        return RedirectToAction("ListaPlanillas");
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

        //GET: Planilla/EliminarPlanilla
        public ActionResult EliminarPlanilla(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        planillaBL.eliminarPlanilla(id);
                        return RedirectToAction("ListaPlanillas");
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