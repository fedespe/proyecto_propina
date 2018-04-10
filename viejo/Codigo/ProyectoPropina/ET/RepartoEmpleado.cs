using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class RepartoEmpleado
    {
        public double Ausencias { get; set; }
        public List<CargoMes> CargosMes { get; set; }
        public double AusenciasAsamblea { get; set; }
        public double MontoPesos { get; set; }
        public double MontoDolares { get; set; }
        public double DescuentoPesos { get; set; }
        public double DescuentoDolares { get; set; }
        public double MontoDiferenciaCargoPesos { get; set; }
        public double MontoDiferenciaCargoDolares { get; set; }
        public double BonoPesos { get; set; }
        public double BonoDolares { get; set; }
        public double PagoExtraPesos { get; set; }
        public double PagoExtraDolares { get; set; }
        public string Comentario { get; set; }

        public RepartoEmpleado() {
            this.CargosMes = new List<CargoMes>();
        }
    }
}
