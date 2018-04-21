using ET;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PropinaWeb.ViewModel.PlanillaViewModel
{
    public class AltaPlanillaViewModel
    {
        public Planilla planilla { get; set; }
        [Required]
        [Display(Name = "Texto")]
        public string Texto { get; set; }
        public bool Firma { get; set; }
        public bool PuedeFirmar { get; set; }
        public Empleado Empleado { get; set; }


        public AltaPlanillaViewModel() {
            planilla = new Planilla();
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
            planilla.empleado.Id = 3;
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