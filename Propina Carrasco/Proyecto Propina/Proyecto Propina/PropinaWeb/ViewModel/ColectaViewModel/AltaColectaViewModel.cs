using BL;
using ET;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropinaWeb.ViewModel.ColectaViewModel
{
    public class AltaColectaViewModel
    {
        private EmpleadoBL empleadoBL = new EmpleadoBL();
        public Colecta colecta { get; set; }
        [Required]
        [Display(Name = "Texto")]
        public string Texto { get; set; }
        public bool Firma { get; set; }
        public bool PuedeFirmar { get; set; }
        public ColectaEmpleado EmpleadoColecta { get; set; }
        public List<Empleado> Empleados { get; set; }
        public int IdSeleccionado { get; set; }
        public SelectList ListaEmpleados { get; set; }
        public int CantidadEmpleados88 { get; set; }
        public double PorcentajeFirmas { get; set; }


        public AltaColectaViewModel()
        {
            colecta = new Colecta();
            EmpleadoColecta = new ColectaEmpleado();
            List<Empleado> empleados = empleadoBL.obtenerTodos();
            foreach (Empleado e in empleados) { e.Datos = "Nº: " + e.NumeroEmpleado + " " + e.Nombre + ", " + e.Apellido; }
            ListaEmpleados = new SelectList(empleados, "Id", "Datos");
        }

        internal void completarAltaPlanillaVM()
        {
            //aca hay que traer todos los empleados de mesas y cajas
            throw new NotImplementedException();
        }

        internal void completarPlanilla()
        {
            colecta.Texto = Texto;
            colecta.EmpleadoColecta = new ColectaEmpleado();
            colecta.EmpleadoColecta.Empleado = new Empleado();
            colecta.EmpleadoColecta.Empleado.Id = IdSeleccionado;
            colecta.Habilitada = true;
            colecta.Eliminado = false;
        }

        internal void comprobarFirmas()
        {
            Firma = colecta.EmpleadosColecta.Contains(EmpleadoColecta);
            if (colecta.EmpleadoColecta.Empleado.Id != EmpleadoColecta.Empleado.Id)
            {
                PuedeFirmar = true;
            }
            else {
                PuedeFirmar = false;
            }
        }
    }
}