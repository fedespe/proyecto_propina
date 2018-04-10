using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CargoBL
    {
        private CargoDAL cargoDAL = new CargoDAL();
        public List<Cargo> obtenerTodos() {
            return CargoDAL.obtenerTodos();
        }
    }
}
