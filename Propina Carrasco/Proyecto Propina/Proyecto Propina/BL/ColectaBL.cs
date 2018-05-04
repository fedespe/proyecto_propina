using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ColectaBL
    {
        private ColectaDAL colectaDAL = new ColectaDAL();
        public List<Colecta> obtenerTodos()
        {
            return colectaDAL.obtenerTodos();
        }

        public void altaColecta(Colecta colecta)
        {
            colectaDAL.altaColecta(colecta);
        }

        public Colecta obtener(int id)
        {
            return colectaDAL.obtener(id);
        }

        public void Colaborar(Colecta colecta)
        {
            colectaDAL.colaborar(colecta);
        }

        public void quitarColaboracion(int idColecta, int idEmpleado)
        {
            colectaDAL.quitarColaboracion(idColecta,idEmpleado);
        }

        public void habilitarColecta(int id)
        {
            colectaDAL.habilitarColecta(id);
        }

        public void deshabilitarColecta(int id)
        {
            colectaDAL.deshabilitarColecta(id);
        }

        public void eliminarColecta(int id)
        {
            colectaDAL.eliminarColecta(id);
        }
    }
}
