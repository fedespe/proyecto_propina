using BL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPropina.ViewModel.RepartoViewModel
{
    public class RepartoViewModel
    {
        private EmpleadoBL empleadoBL = new EmpleadoBL();
        private CargoBL cargoBL = new CargoBL();
        public Reparto Reparto { get; set; }
        public string TxtFaltasTotales { get; set; }

        public RepartoViewModel()
        {
            this.Reparto = new Reparto();
            this.Reparto.Empleados = empleadoBL.obtenerTodos();
            List<Cargo> cargos = cargoBL.obtenerTodos();
            //Le agrego a todos los empleados todos los cargos, 
            //asi los puedo manejar desde javascript
            foreach(Empleado e in this.Reparto.Empleados)
            {
                foreach (Cargo c in cargos)
                {
                    CargoMes cm = new CargoMes
                    {
                        Cargo = c,
                        DiasTrabajados = 0
                    };
                    e.RepartoEmpleado.CargosMes.Add(cm);
                }    
            }

        }
    }
}