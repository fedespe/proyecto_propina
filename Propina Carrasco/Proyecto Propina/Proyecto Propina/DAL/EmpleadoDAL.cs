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

        public List<Empleado> obtenerTodos()
        {
            List<Empleado> empleados = new List<Empleado>();
            string cadenaSelect = "SELECT u.*,e.* FROM Usuario u, Empleado e WHERE u.Id=e.EmpleadoId;";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(cadenaSelect, con))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Empleado empleado = new Empleado
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    NumeroEmpleado = Convert.ToInt32(dr["NumeroEmpleado"])
                                };
                                empleados.Add(empleado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }

            return empleados;
        }

        public Empleado obtener(int id)
        {
            Empleado emp = null;
            string cadenaSelectUsuario = "SELECT u.*, c.Nombre as NombreCargo FROM Usuario u, Cargo c, Empleado e WHERE u.Id=e.EmpleadoId AND e.CargoId=c.Id AND u.Id=@id AND Tipo='EMPLEADO'";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaSelectUsuario, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
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
                                    Tipo = dr["Tipo"].ToString(),
                                    Cargo= new Cargo { Nombre= dr["NombreCargo"].ToString() }
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
