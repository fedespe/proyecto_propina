using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using System.Data.SqlClient;

namespace DAL
{
    public class EmpleadoDAL
    {
        public Empleado ingresarEmpleado(string nombreUsu, string pass)
        {
            Empleado emp = null;
            string cadenaSelectUsuario = "SELECT * FROM Usuario WHERE NombreUsuario=@nomUsu AND Contrasenia=@pass AND Habilitado=1 AND Tipo='EMPLEADO'";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaSelectUsuario, con))
                    {
                        cmd.Parameters.AddWithValue("@nomUsu", nombreUsu);
                        //cmd.Parameters.AddWithValue("@pass", Utilidades.calcularMD5Hash(pass));
                        cmd.Parameters.AddWithValue("@pass", pass);
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            if (dr.HasRows)
                            {
                                emp = new Empleado
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    NombreUsuario = dr["NombreUsuario"].ToString(),
                                    Contrasena = dr["Contrasenia"].ToString(),                                  
                                    Habilitado = Convert.ToBoolean(dr["Habilitado"]),
                                    CorreoElectronico = dr["Email"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Direccion = dr["Direccion"].ToString(),
                                    Tipo = dr["Tipo"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }

            return emp;
        
        }
    }
}
