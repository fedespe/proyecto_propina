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
        public double AusenciasAnteriores { get; set; }
        public double AusenciasAnterioresAsamblea { get; set; }
        public Cargo Cargo { get; set; }
        public bool Eventual { get; set; }
        public double DiasTrabajadosEventuales { get; set; }
        public DateTime FechaIngreso { get; set; }
        public RepartoEmpleado RepartoEmpleado { get; set; }
    }
}
