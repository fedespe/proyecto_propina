using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropinaWeb.ViewModel.RepartoViewModel
{
    public class EditarRepartoDiarioViewModel
    {
        public RepartoDiario RepartoDiario { get; set; }
        public DateTime Fecha { get; set; }

        internal void completarReparto()
        {
            RepartoDiario.Fecha = Fecha;
        }
    }
}