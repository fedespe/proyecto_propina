using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using System.Data.SqlClient;

namespace DAL
{
    public class ColectaDAL
    {
        public List<Colecta> obtenerTodos()
        {
            List<Colecta> colectas = new List<Colecta>();
            string cadenaSelectPregunta = "SELECT c.*,u.Nombre, u.Apellido  FROM COLECTA c, USUARIO u WHERE c.IdEmpleado= u.Id AND c.eliminado = 0;";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(cadenaSelectPregunta, con))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Colecta colecta = new Colecta
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Texto = dr["Texto"].ToString(),
                                    Fecha = Convert.ToDateTime(dr["FechaAlta"]),
                                    EmpleadoColecta = new ColectaEmpleado
                                    {
                                        Empleado=new Empleado
                                        {
                                            Id = Convert.ToInt32(dr["IdEmpleado"]),
                                            Nombre = dr["Nombre"].ToString(),
                                            Apellido = dr["Apellido"].ToString(),
                                        }
                                    },
                                    Habilitada = Convert.ToBoolean(dr["Habilitada"]),
                                    Eliminado = Convert.ToBoolean(dr["Eliminado"]),
                                };
                                colectas.Add(colecta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }

            return colectas;
        }

        public void eliminarColecta(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Update Colecta SET Eliminado = 1 WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public void deshabilitarColecta(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Update Colecta SET Habilitada = 0 WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public void habilitarColecta(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Update Colecta SET Habilitada = 1 WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public void quitarColaboracion(int idColecta, int idEmpleado)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Delete COLECTAEMPLEADO WHERE @idEmpleado=EmpleadoId AND @idColecta=ColectaId;", con);
                    cmd.Parameters.AddWithValue("@idColecta", idColecta);
                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public void colaborar(Colecta colecta)
        {
            string cadenaInsertPregunta = @"INSERT INTO COLECTAEMPLEADO VALUES(@idEmpleado, @idColecta, @pesos, @dolares);";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaInsertPregunta, con))
                    {
                        cmd.Parameters.AddWithValue("@idEmpleado", colecta.EmpleadoColecta.Empleado.Id);
                        cmd.Parameters.AddWithValue("@idColecta", colecta.Id);
                        cmd.Parameters.AddWithValue("@pesos", colecta.EmpleadoColecta.MontoPesos);
                        cmd.Parameters.AddWithValue("@dolares", colecta.EmpleadoColecta.MontoDolares);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public Colecta obtener(int id)
        {
            Colecta colecta = null;
            string cadenaSelectPlanilla = "SELECT * FROM COLECTA WHERE id=@id";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(cadenaSelectPlanilla, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            if (dr.HasRows)
                            {
                                colecta = new Colecta
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Texto = dr["Texto"].ToString(),
                                    Fecha = Convert.ToDateTime(dr["FechaAlta"]),
                                    Habilitada = Convert.ToBoolean(dr["Habilitada"]),
                                    Eliminado = Convert.ToBoolean(dr["Eliminado"]),
                                    EmpleadoColecta = new ColectaEmpleado { Empleado=new Empleado { Id = Convert.ToInt32(dr["IdEmpleado"]) } },
                                    EmpleadosColecta = new List<ColectaEmpleado>()
                                };
                                //FALTA TRAER TODOS LOS EMPLEADOS QUE FIRMARON
                            }
                        }
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT c.*, u.* FROM COLECTAEMPLEADO c, USUARIO u WHERE c.ColectaId=@idColecta AND c.EmpleadoId=u.Id;";
                        cmd.Parameters.AddWithValue("@idColecta", id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                colecta.EmpleadosColecta.Add(new ColectaEmpleado
                                {
                                    Empleado= new Empleado {
                                        Id = Convert.ToInt32(dr["EmpleadoId"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Apellido = dr["Apellido"].ToString(),
                                    },
                                    MontoPesos= Convert.ToDouble(dr["MontoPesos"]),
                                    MontoDolares = Convert.ToDouble(dr["MontoDolares"]),
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }

            return colecta;
        }

        public void altaColecta(Colecta colecta)
        {
            string cadenaInsertPregunta = @"INSERT INTO Colecta VALUES(@texto, @fecha, @idEmp, @hab, @eli);";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaInsertPregunta, con))
                    {
                        cmd.Parameters.AddWithValue("@texto", colecta.Texto);
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@idEmp", colecta.EmpleadoColecta.Empleado.Id);
                        cmd.Parameters.AddWithValue("@hab", colecta.Habilitada);
                        cmd.Parameters.AddWithValue("@eli", colecta.Eliminado);

                        con.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }
    }
}
