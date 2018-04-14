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
        public Cargo Cargo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Empleado)
            {
                Empleado e = (Empleado)obj;
                return e.Id == this.Id;
            }
            return false;
        }
    }
}
