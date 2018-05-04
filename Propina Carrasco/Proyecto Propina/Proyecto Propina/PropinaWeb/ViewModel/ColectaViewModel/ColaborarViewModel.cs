using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropinaWeb.ViewModel.ColectaViewModel
{
    public class ColaborarViewModel
    {
       // public int IdColecta { get; set; }
        public Colecta Colecta { get; set; }
        public double MontoPesos { get; set; }
        public double MontoDolares { get; set; }


        public ColaborarViewModel() {
            Colecta = new Colecta();
            Colecta.EmpleadoColecta = new ColectaEmpleado();
        }

    }
}