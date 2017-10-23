using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public bool Habilitado { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
        public string correoElectronico { get; set; }
        //Administrador, Empleado, Visitante
        public string Tipo { get; set; }

    }
}
