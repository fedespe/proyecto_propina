using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Cargo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Puntaje { get; set; }
        //Indica se pertenece al 12 o al 88
        public string Tipo { get; set; }
        //Indica si pertenece a Mesas, Cajas u Otros
        public string Area { get; set; }

    }
}
