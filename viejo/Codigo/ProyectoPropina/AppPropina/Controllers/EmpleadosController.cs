using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPropina.Controllers
{
    public class EmpleadosController : Controller
    {
        private EmpleadoBL empleadoBL = new EmpleadoBL();
        public JsonResult ObtenerTodos()
        {
            List<Empleado> model = new List<Empleado>();

            //if (Session["TipoUsuario"] != null && Session["TipoUsuario"].ToString().Equals("Administrador"))
            //{
                try
                {
                    model = empleadoBL.obtenerTodos();
                }
                catch (ProyectoException ex)
                {
                    //En caso de que haya excepción no necesitaría hacer nada en un principio, ya que me va a retornar el model vacío
                }
            //}

            return Json(model, JsonRequestBehavior.AllowGet); //Para que es el AllowGet?
        }



        public class Coche
        {
            public string Marca { get; set; }
            public string Modelo { get; set; }
        }
        //public class Marca {
        //    public int Id { get; set; }
        //    public string Nombre { get; set; }
        //}

        [HttpPost]
        // El Json recibido será serializado automáticamente al objeto nuevo empleados teniendo en cuenta que las propiedades han de tener el mismo nombre
        public JsonResult EmpleadosReparto(List<Empleado> Empleados)
        //public JsonResult EmpleadosReparto(IList<Coche> Coche)
        {
            if (Empleados != null)
                return Json("'Success':'true'");
            else
                return Json(String.Format("'Success':'false','Error':'Ha habido un error al insertar el registro.'"));
        }
    }
}