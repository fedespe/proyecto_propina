using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ColectaEmpleado
    {
        public Empleado Empleado { get; set; }
        public double MontoPesos { get; set; }
        public double MontoDolares { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ColectaEmpleado)
            {
                ColectaEmpleado e = (ColectaEmpleado)obj;
                return e.Empleado.Id == this.Empleado.Id;
            }
            return false;
        }
    }
}
