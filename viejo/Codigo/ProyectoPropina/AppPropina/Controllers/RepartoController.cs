using AppPropina.ViewModel.RepartoViewModel;
using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPropina.Controllers
{
    public class RepartoController : Controller
    {
        private EmpleadoBL empleadoBL = new EmpleadoBL();
        // GET: Reparto
        public ActionResult Reparto()
        {
            RepartoViewModel repartoVM = new RepartoViewModel();
            return View(repartoVM);
        }

        // GET: Reparto
        [HttpPost]
        public ActionResult Reparto(RepartoViewModel repartoVM)
        {
            return View();
        }

        public JsonResult ObtenerNuevoReparto()
        {
            Reparto model = null;
            //if (Session["TipoUsuario"] != null && Session["TipoUsuario"].ToString().Equals("Administrador"))
            //{
            try
            {
                RepartoViewModel repartoVM = new RepartoViewModel();
                model = repartoVM.Reparto;
            }
            catch (ProyectoException ex)
            {
                //En caso de que haya excepción no necesitaría hacer nada en un principio, ya que me va a retornar el model vacío
            }
            //}

            return Json(model, JsonRequestBehavior.AllowGet); //Para que es el AllowGet?
        }

        [HttpPost]
        // El Json recibido será serializado automáticamente al objeto nuevo empleados teniendo en cuenta que las propiedades han de tener el mismo nombre
        public JsonResult IngresarReparto(Reparto Reparto)
        {
            if (Reparto != null) {
                //return Json("'Success':'true'");
                Reparto.FondoPesos = Reparto.MontoPesos * 0.02;
                Reparto.FondoDolares = Reparto.MontoDolares * 0.02;
                return Json(Reparto, JsonRequestBehavior.AllowGet); //Para que es el AllowGet?
            }  
            else
                return Json(String.Format("'Success':'false','Error':'Ha habido un error al insertar el registro.'"));
        }
    }
}