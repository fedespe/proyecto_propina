using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using DAL;

namespace BL
{
    public class RepartoBL
    {
        private RepartoDAL repartoDAL = new RepartoDAL();
        public List<Reparto> obtenerTodos()
        {
            return repartoDAL.obtenerTodos();
        }

        public Reparto obtener(int id)
        {
            return repartoDAL.obtener(id);
        }

        public void actualizarReparto(Reparto reparto)
        {
            repartoDAL.actualizarReparto(reparto);
        }

        public void altaReparto(Reparto reparto)
        {
            repartoDAL.altaReparto(reparto);
        }
    }
}
