using BL;
using ET;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropinaWeb.ViewModel.PlanillaViewModel
{
    public class AltaPlanillaViewModel
    {
        private EmpleadoBL empleadoBL = new EmpleadoBL();
        public Planilla planilla { get; set; }
        [Required]
        [Display(Name = "Texto")]
        public string Texto { get; set; }
        public bool Firma { get; set; }
        public bool PuedeFirmar { get; set; }
        public Empleado Empleado { get; set; }
        public List<Empleado> Empleados { get; set; }
        public int IdSeleccionado { get; set; }
        public SelectList ListaEmpleados { get; set; }


        public AltaPlanillaViewModel() {
            planilla = new Planilla();
            List<Empleado> empleados = empleadoBL.obtenerTodos();
            foreach (Empleado e in empleados) { e.Datos = "Nº: " + e.NumeroEmpleado + " " + e.Nombre + ", " + e.Apellido; }
            ListaEmpleados = new SelectList(empleados,"Id","Datos");
        }

        internal void completarAltaPlanillaVM()
        {
            //aca hay que traer todos los empleados de mesas y cajas
            throw new NotImplementedException();
        }

        internal void completarPlanilla()
        {
            planilla.Texto = Texto;
            planilla.empleado = new Empleado();
            planilla.empleado.Id = IdSeleccionado;
            planilla.Habilitada = true;
            planilla.Eliminado = false;
        }

        internal void comprobarFirmas()
        {
            Firma = planilla.Empleados.Contains(Empleado);
            if (planilla.empleado.Id != Empleado.Id && (Empleado.Cargo.Nombre.Equals("CROUPIER") || Empleado.Cargo.Nombre.Equals("SUPERVIDOR_MESAS") 
                || Empleado.Cargo.Nombre.Equals("CAJERO") || Empleado.Cargo.Nombre.Equals("SUPERVISOR_CAJAS")))
            {
                PuedeFirmar = true;
            }
            else {
                PuedeFirmar = false;
            }
        }
    }
}