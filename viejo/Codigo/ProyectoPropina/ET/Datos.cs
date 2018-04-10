using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Datos
    {
        public Administrador Administrador { get; set; }
        public Reparto UltimoReparto { get; set; }
        public double MontoFondoPesos { get; set; }
        public double MontoFondoDolares { get; set; }
        public double SalarioIntegranteComisionPesos { get; set; }
        public double SalarioIntegranteComisionDolalares { get; set; }
        public double PorcentajeMayor { get; set; }
        public double PorcentajeMenor { get; set; }
        public double PorcentajeFondo { get; set; }
    }
}
