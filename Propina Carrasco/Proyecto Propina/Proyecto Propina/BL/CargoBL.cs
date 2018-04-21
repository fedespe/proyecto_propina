using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;

namespace BL
{
    public class CargoBL
    {
        private CargoDAL cargoDAL = new CargoDAL();

        public List<Cargo> obtenerTodos()
        {
            return cargoDAL.obtenerTodos();
        }
    }
}
