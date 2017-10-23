using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Empleado : Usuario
    {
        public int NumeroEmpleado { get; set; }
        public double ausenciasAnteriores { get; set; }
        public double ausenciasAnterioresAsamblea { get; set; }
        public double ausenciasNuevas { get; set; }
        public double ausenciasNuevasAsamblea { get; set; }
    }
}
