using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class MensajePrincipalBL
    {
        private MensajePrincipalDAL mensajeDAL = new MensajePrincipalDAL();

        public List<MensajePrincipal> obtenerTodos() {
            return mensajeDAL.obtenerTodos();
        }

        public void eliminarMensaje(int id)
        {
            mensajeDAL.eliminarMensaje(id);
        }

        public void altaMensaje(MensajePrincipal mensaje)
        {
            mensajeDAL.altaMensaje(mensaje);
        }
    }
}
