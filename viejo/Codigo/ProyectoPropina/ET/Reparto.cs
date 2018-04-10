using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Reparto
    {
        public DateTime FechaReparto { get; set; }
        public Administrador Administrador { get; set; }
        public double MontoPesos { get; set; }
        public double MontoDolares { get; set; }
        public List<Empleado> Empleados { get; set; }
        public double FondoPesos { get; set; }
        public double FondoDolares { get; set; }
    }
}
