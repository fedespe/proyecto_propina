using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using DAL;

namespace BL
{
    public class RepartoDiarioBL
    {
        private RepartoDiarioDAL repartoDiarioDAL = new RepartoDiarioDAL();
        public RepartoDiario obtener(int id)
        {
           return repartoDiarioDAL.obtener(id);
        }

        public void actualizarRepartoDiario(RepartoDiario repartoDiario)
        {
            repartoDiarioDAL.actualizarRepartoDiario(repartoDiario);
        }

        public void altaRepartoDiario(RepartoDiario repartoDiario)
        {
            repartoDiarioDAL.altaRepartoDiario(repartoDiario);
        }
    }
}
