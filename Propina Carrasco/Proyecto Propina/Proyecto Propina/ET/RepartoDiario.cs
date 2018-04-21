using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class RepartoDiario
    {
        public int Id { get; set; }
        public Reparto Reparto { get; set; }
        public DateTime Fecha { get; set; }
        public double MontoPesosMesas { get; set; }
        public double MontoDolaresMesas { get; set; }
        public double MontoPesosOtros { get; set; }
        public double MontoDolaresOtros { get; set; }
    }
}
