using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EmpleadoBL
    {
        private EmpleadoDAL empleadoDAL = new EmpleadoDAL();
        public Empleado ingresarEmpleado(string nombreUsu, string pass)
        {
            return empleadoDAL.ingresarEmpleado(nombreUsu, pass);
        }

        public Empleado obtener(int id)
        {
            return empleadoDAL.obtener(id);
        }

        public List<Empleado> obtenerTodos()
        {
            return empleadoDAL.obtenerTodos();
        }

        public int obtenerCantidadEmpleados88()
        {
            return empleadoDAL.obtenerCantidadEmpleados88();
        }
    }
}
