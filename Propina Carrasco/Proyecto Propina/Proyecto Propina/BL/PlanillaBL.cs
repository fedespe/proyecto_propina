using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PlanillaBL
    {
        private PlanillaDAL planillaDAL = new PlanillaDAL();

        public void altaPlanilla(Planilla pla)
        {
            validarPlanilla(pla);
            planillaDAL.altaPlanilla(pla);
        }

        private void validarPlanilla(Planilla pla)
        {
            //throw new NotImplementedException();
        }

        public List<Planilla> obtenerTodos()
        {
            return planillaDAL.obtenerTodos();
        }

        public void altaPlanilla(object planilla)
        {
            throw new NotImplementedException();
        }

        public Planilla obtener(int id)
        {
            throw new NotImplementedException();
        }

        public void actualizarPlanilla(Planilla planilla)
        {
            throw new NotImplementedException();
        }
    }
}
