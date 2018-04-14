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
                    return View(planillaBL.obtenerTodos());
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
                        viewModel.Empleado = new Empleado { Id= Convert.ToInt32(Session["IdUsuario"]) };
                        EmpleadoBL emp = new EmpleadoBL();
                        viewModel.planilla.empleado = emp.obtener(viewModel.planilla.empleado.Id);
                        viewModel.comprobarFirmas();
                        //viewModel.completarAltaPlanillaVM();
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
                        return View("~/Views/Shared/_Mensajes.cshtml");
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
                        return View("~/Views/Shared/_Mensajes.cshtml");
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











        //GET: Planilla/Editar
        public ActionResult Editar(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        AltaPlanillaViewModel editVM = new AltaPlanillaViewModel();
                        editVM.planilla = planillaBL.obtener(id);
                        editVM.completarAltaPlanillaVM();
                        return View(editVM);
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

        //POST: Planilla/Editar
        [HttpPost]
        public ActionResult Editar(AltaPlanillaViewModel editVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    editVM.completarPlanilla();
                    planillaBL.actualizarPlanilla(editVM.planilla);

                    return RedirectToAction("ListaPlanillas");

                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
            else {
                return View(editVM);
            }
        }
    }
}