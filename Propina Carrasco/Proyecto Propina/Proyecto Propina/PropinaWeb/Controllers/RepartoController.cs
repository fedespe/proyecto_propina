using BL;
using ET;
using PropinaWeb.ViewModel.RepartoViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropinaWeb.Controllers
{
    public class RepartoController : Controller
    {
        private RepartoBL repartoBL = new RepartoBL();
        private RepartoDiarioBL repartoDiarioBL = new RepartoDiarioBL();

        public ActionResult MostrarDatos()
        {
            if (Session["TipoUsuario"] != null)// && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    MostrarDatosViewModel vm = new MostrarDatosViewModel();
                    vm.cargarDatos();
                    return View(vm);
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


        //GET: Reparto/EditarReparto
        public ActionResult EditarReparto(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        EditarRepartoViewModel editVM = new EditarRepartoViewModel();
                        editVM.Reparto = repartoBL.obtener(id);
                        editVM.Reparto.RepartosDiarios.OrderBy(p => p.Fecha).ToList();
                        editVM.Fecha = editVM.Reparto.Fecha;
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

        //POST: Reparto/EditarReparto
        [HttpPost]
        public ActionResult EditarReparto(EditarRepartoViewModel editVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    editVM.completarReparto();
                    repartoBL.actualizarReparto(editVM.Reparto);

                    return RedirectToAction("MostrarDatos");

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

        //GET: Reparto/EditarReparto
        public ActionResult EditarRepartoDiario(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        EditarRepartoDiarioViewModel editVM = new EditarRepartoDiarioViewModel();
                        editVM.RepartoDiario = repartoDiarioBL.obtener(id);
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

        //POST: Reparto/EditarRepartoDiario
        [HttpPost]
        public ActionResult EditarRepartoDiario(EditarRepartoDiarioViewModel editVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    repartoDiarioBL.actualizarRepartoDiario(editVM.RepartoDiario);

                    return RedirectToAction("EditarReparto","Reparto",new { id = editVM.RepartoDiario.Reparto.Id});//enviar a editar reparto con idreparto

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

        //GET: Reparto/AltaReparto
        public ActionResult AltaReparto()
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    return View(new Reparto());
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

        //POST: Reparto/AltaReparto
        [HttpPost]
        public ActionResult AltaReparto(Reparto reparto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //reparto.completarPlanilla();
                    repartoBL.altaReparto(reparto);
                    return RedirectToAction("MostrarDatos");
                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
            else {
                return View(reparto);
            }
        }

        //GET: Reparto/AltaRepartoDiario
        public ActionResult AltaRepartoDiario(int id=0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        RepartoDiario repartoD = new RepartoDiario();
                        repartoD.Reparto = new Reparto { Id = id };
                        return View(repartoD);
                    }
                    else {
                        ViewBag.Mensaje = "No seleccionó un reparto valido.";
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

        //POST: Reparto/AltaRepartoDiario
        [HttpPost]
        public ActionResult AltaRepartoDiario(RepartoDiario repartoDiario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //reparto.completarPlanilla();
                    repartoDiarioBL.altaRepartoDiario(repartoDiario);
                    return RedirectToAction("EditarReparto", "Reparto", new { id = repartoDiario.Reparto.Id });//enviar a editar reparto con idreparto
                }
                catch (ProyectoException ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    return View("~/Views/Shared/_Mensajes.cshtml");
                }
            }
            else {
                return View(repartoDiario);
            }
        }

        //GET: Reparto/ActivarReparto
        public ActionResult ActivarReparto(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        repartoBL.activarReparto(id);
                        return RedirectToAction("MostrarDatos");
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

        //GET: Reparto/DesactivarReparto
        public ActionResult DesactivarReparto(int id = 0)
        {
            if (Session["TipoUsuario"] != null && (Session["TipoUsuario"].ToString().Equals("ADMINISTRADOR")))
            {
                try
                {
                    if (id != 0)
                    {
                        repartoBL.desactivarReparto(id);
                        return RedirectToAction("MostrarDatos");
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