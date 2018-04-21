using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropinaWeb.ViewModel.RepartoViewModel
{
    public class EditarRepartoViewModel
    {
        public Reparto Reparto { get; set; }
        public DateTime Fecha { get; set; }

        internal void completarReparto()
        {
            Reparto.Fecha = Fecha;
        }
    }
}