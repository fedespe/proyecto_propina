﻿using System;
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
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Tipo { get; set; }
    }
}
