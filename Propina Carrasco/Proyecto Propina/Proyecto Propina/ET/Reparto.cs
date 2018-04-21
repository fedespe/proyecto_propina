using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Reparto
    {
        public int Id { get; set; }
        public double MontoTotalPesos { get; set; }
        public double MontoTotalDolares { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
        public List<RepartoDiario> RepartosDiarios { get; set; }
    }
}
