using DAL;
using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EmpleadoBL: UsuarioBL
    {
        private EmpleadoDAL empleadoDAL = new EmpleadoDAL();

        public List<Empleado> obtenerTodos()
        {
            return empleadoDAL.obtenerTodos();
        }
        public Empleado obtener(int id)
        {
            return empleadoDAL.obtener(id);
        }

        //public void altaCliente(Empleado empleado)
        //{
        //    base.validarUsuario(empleado);
        //    empleadoDAL.alta(empleado);
        //}
        //public void actualizarCliente(Empleado empleado)
        //{
        //    base.validarActualizacionUsuario(empleado);
        //    empleadoDAL.actualizar(empleado);
        //}
        //public void actualizarContrasena(int id, string contrasenaAnterior, string contrasenaNueva)
        //{
        //    base.validarContrasena(contrasenaNueva);
        //    if (contrasenaAnterior.Equals(contrasenaNueva))
        //    {
        //        throw new ProyectoException("Error: Contraseña anterior igual a nueva");
        //    }
        //    Empleado empleado = empleadoDAL.obtener(id);
        //    if (empleado == null)
        //    {
        //        throw new ProyectoException("Error: Cliente no encontrado");
        //    }
        //    if (!Utilidades.calcularMD5Hash(contrasenaAnterior).Equals(empleado.Contrasena))
        //    {
        //        throw new ProyectoException("Error: Contraseña anterior");
        //    }
        //    empleadoDAL.actualizarContrasena(id, contrasenaNueva);
        //}

        //Para uso del administrador en caso de perdida de la misma por parte del cliente
        //public void nuevaContrasena(int id, string contrasenaNueva)
        //{
        //    base.validarContrasena(contrasenaNueva);
        //    Empleado empleado = empleadoDAL.obtener(id);
        //    if (empleado == null)
        //    {
        //        throw new ProyectoException("Error: Cliente no encontrado");
        //    }
        //    empleadoDAL.actualizarContrasena(id, contrasenaNueva);
        //}       
        //public void habilitar(int id)
        //{
        //    empleadoDAL.habilitarUsuario(id);
        //}
        //public void deshabilitar(int id)
        //{
        //    empleadoDAL.deshabilitarUsuario(id);
        //}
        //public Empleado ingresar(string nombreUsu, string pass)
        //{
        //    return empleadoDAL.ingresar(nombreUsu, pass);
        //}
        //public Empleado obtener(string email)
        //{
        //    return empleadoDAL.obtener(email);
        //}
        //public Empleado recuperarPassword(string email)
        //{
        //    Cliente cliente = this.obtener(email);
        //    if (cliente != null)
        //    {
        //        string passwordGenerado = Utilidades.generarPassword(10);
        //        this.empleadoDAL.actualizarContrasena(cliente.Id, passwordGenerado);
        //        List<string> destinatariosCorreo = new List<string>();
        //        destinatariosCorreo.Add(cliente.CorreoElectronico);
        //        string cuerpoCorreo = "Su nueva constraseña es: <strong>" + passwordGenerado + "</strong><br>Sugerimos cambiarla luego de ingresar al sistema.";
        //        Utilidades.enviarCorreo(destinatariosCorreo, "SPODS", "Recuperación de Contraseña", cuerpoCorreo);
        //    }
        //    else {
        //        throw new ProyectoException("Error: No existe un usuario con el correo indicado.");
        //    }
        //    return cliente;
        //}
    }
}
