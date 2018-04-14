using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Planilla
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime Fecha { get; set; }
        public List<Empleado> Empleados { get; set; }
        public bool Habilitada { get; set; }
        public Empleado empleado { get; set; }
        public bool Eliminado { get; set; }
    }
}
