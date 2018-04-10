using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UsuarioBL
    {
        private UsuarioDAL usuarioDAL = new UsuarioDAL();

        public Usuario ingresarUsuario(string nombreUsu, string pass)
        {
            return usuarioDAL.ingresarUsuario(nombreUsu, pass);
        }
    }
}
