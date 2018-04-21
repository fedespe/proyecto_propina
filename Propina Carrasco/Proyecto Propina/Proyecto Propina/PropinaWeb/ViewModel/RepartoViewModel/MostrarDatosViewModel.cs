using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropinaWeb.ViewModel.RepartoViewModel
{
    public class MostrarDatosViewModel
    {
        public List<Reparto> Repartos { get; set; }
        private RepartoBL repartoBL = new RepartoBL();
        public MostrarDatosViewModel() {
            Repartos = new List<Reparto>();
        }
        public void cargarDatos()
        {
            Repartos = repartoBL.obtenerTodos();
        }
    }
}