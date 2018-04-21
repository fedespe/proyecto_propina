using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PropinaWeb.ViewModel.RepartoViewModel
{
    public class MostrarDatosViewModel
    {
        public List<Reparto> Repartos { get; set; }
        private RepartoBL repartoBL = new RepartoBL();
        private CargoBL cargoBL = new CargoBL();
        public Reparto RepartoActivo { get; set; }
        //Agregar montos totales del rep actual
        public double MontoTotalPesos { get; set; }
        public double MontoTotalDolares { get; set; }
        public List<Cargo> Cargos { get; set; }
        public MostrarDatosViewModel() {
            Repartos = new List<Reparto>();
        }
        public void cargarDatos()
        {
            Repartos = repartoBL.obtenerTodos().OrderByDescending(p => p.Fecha).ToList();
            if (Repartos.Where(p => p.Activo).FirstOrDefault() != null) {
                int idAct = Repartos.Where(p => p.Activo).FirstOrDefault().Id;
                RepartoActivo = repartoBL.obtener(idAct);
                foreach (RepartoDiario d in RepartoActivo.RepartosDiarios) {
                    MontoTotalPesos += d.MontoPesosMesas + d.MontoPesosOtros;
                    MontoTotalDolares += d.MontoDolaresMesas + d.MontoDolaresOtros;
                }
            }
            cargarAproximados();
        }
        public void cargarAproximados() {
            Cargos = cargoBL.obtenerTodos();
            double pesos88 = (MontoTotalPesos * 0.98) * 0.88;
            double dolares88 = (MontoTotalDolares * 0.98) * 0.88;
            double pesos12 = (MontoTotalPesos * 0.98) * 0.12;
            double dolares12 = (MontoTotalDolares * 0.98) * 0.12;
            int totalCroupier = 35;
            int totalSupMesas = 15;
            int totalCajero = 7;
            int totalSupCaja = 3;
            int totalOtros = 36;
            int pCroupier = 30;
            int pSupMesas = 23;
            int pCajero = 14;
            int pSupCaja = 14;
            int pOtros = 10;
            int TotalPuntos88 = (totalCroupier * pCroupier) +(totalSupMesas * pSupMesas) +(totalCajero * pCajero) + (totalSupCaja * pSupCaja);
            int TotalPuntos12 = (totalOtros * pOtros);
            double ValorPunto88Pesos = pesos88 / TotalPuntos88;
            double ValorPunto12Pesos = pesos12 / TotalPuntos12;
            double ValorPunto88Dolares = dolares88 / TotalPuntos88;
            double ValorPunto12Dolares = dolares12 / TotalPuntos12;


            foreach (Cargo c in Cargos) {
                if (c.Nombre.Equals("CROUPIER")) {
                    c.AproximadoPesos = pCroupier * ValorPunto88Pesos;
                    c.AproximadoDolares = pCroupier * ValorPunto88Dolares;
                }
                else if (c.Nombre.Equals("SUPERVIDOR_MESAS")) {
                    c.AproximadoPesos = pSupMesas * ValorPunto88Pesos;
                    c.AproximadoDolares = pSupMesas * ValorPunto88Dolares;
                }
                else if (c.Nombre.Equals("CAJERO"))
                {
                    c.AproximadoPesos = pCajero * ValorPunto88Pesos;
                    c.AproximadoDolares = pCajero * ValorPunto88Dolares;
                }
                else if (c.Nombre.Equals("SUPERVISOR_CAJAS"))
                {
                    c.AproximadoPesos = pSupCaja * ValorPunto88Pesos;
                    c.AproximadoDolares = pSupCaja * ValorPunto88Dolares;
                }
                else if (c.Nombre.Equals("MARKETING"))
                {
                    c.AproximadoPesos = pOtros * ValorPunto12Pesos;
                    c.AproximadoDolares = pOtros * ValorPunto12Dolares;
                }
                else if (c.Nombre.Equals("SLOTS"))
                {
                    c.AproximadoPesos = pOtros * ValorPunto12Pesos;
                    c.AproximadoDolares = pOtros * ValorPunto12Dolares;
                }
                else if (c.Nombre.Equals("TECNICOS_SLOTS"))
                {
                    c.AproximadoPesos = pOtros * ValorPunto12Pesos;
                    c.AproximadoDolares = pOtros * ValorPunto12Dolares;
                }
                else if (c.Nombre.Equals("SOFTCOUNT"))
                {
                    c.AproximadoPesos = pOtros * ValorPunto12Pesos;
                    c.AproximadoDolares = pOtros * ValorPunto12Dolares;
                }
                else if (c.Nombre.Equals("SERVICIOS_GENERALES"))
                {
                    c.AproximadoPesos = pOtros * ValorPunto12Pesos;
                    c.AproximadoDolares = pOtros * ValorPunto12Dolares;
                }
                else if (c.Nombre.Equals("MANTENIMIENTO"))
                {
                    c.AproximadoPesos = pOtros * ValorPunto12Pesos;
                    c.AproximadoDolares = pOtros * ValorPunto12Dolares;
                }
            }
        }
    }
}