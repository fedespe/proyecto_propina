using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using System.Data.SqlClient;

namespace DAL
{
    public class PlanillaDAL
    {
        public void altaPlanilla(Planilla pla)
        {
            string cadenaInsertPregunta = @"INSERT INTO Planilla VALUES(@texto, @fecha, @idEmp, @hab, @eli);";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaInsertPregunta, con))
                    {
                        cmd.Parameters.AddWithValue("@texto", pla.Texto);
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@idEmp", pla.empleado.Id);
                        cmd.Parameters.AddWithValue("@hab", pla.Habilitada);
                        cmd.Parameters.AddWithValue("@eli", pla.Eliminado);

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

        public void quitarFirmarPlanilla(int id, int idEmpleado)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Delete PlanillaEmpleado WHERE @idEmpleado=EmpleadoId AND @idPlanilla=PlanillaId;", con);
                    cmd.Parameters.AddWithValue("@idPlanilla", id);
                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public void habilitarPlanilla(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Update Planilla SET Habilitada = 1 WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }
        public void deshabilitarPlanilla(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Update Planilla SET Habilitada = 0 WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public void firmarPlanilla(int id, int idEmpleado)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Insert into PlanillaEmpleado Values (@idEmpleado, @idPlanilla)", con);
                    cmd.Parameters.AddWithValue("@idPlanilla", id);
                    cmd.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public Planilla obtener(int id)
        {
            Planilla planilla = null;
            string cadenaSelectPlanilla = "SELECT * FROM PLANILLA WHERE id=@id";
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
                                planilla = new Planilla
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Texto = dr["Texto"].ToString(),
                                    Fecha = Convert.ToDateTime(dr["FechaAlta"]),
                                    Habilitada = Convert.ToBoolean(dr["Habilitada"]),
                                    Eliminado = Convert.ToBoolean(dr["Eliminado"]),
                                    empleado = new Empleado { Id = Convert.ToInt32(dr["IdEmpleado"]) },
                                    Empleados = new List<Empleado>()
                                };
                                //FALTA TRAER TODOS LOS EMPLEADOS QUE FIRMARON
                            }
                        }
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT p.*, u.* FROM PLANILLAEMPLEADO p, USUARIO u WHERE p.PlanillaId=@idPlanilla AND p.EmpleadoId=u.Id;";
                        cmd.Parameters.AddWithValue("@idPlanilla", id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                planilla.Empleados.Add(new Empleado {
                                    Id = Convert.ToInt32(dr["EmpleadoId"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
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

            return planilla;
        }

        public List<Planilla> obtenerTodos()
        {
            List<Planilla> planillas = new List<Planilla>();
            string cadenaSelectPregunta = "SELECT p.*,u.Nombre, u.Apellido  FROM PLANILLA p, USUARIO u WHERE p.IdEmpleado= u.Id AND p.eliminado = 0;";
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
                                Planilla planilla = new Planilla
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Texto = dr["Texto"].ToString(),
                                    Fecha = Convert.ToDateTime(dr["FechaAlta"]),
                                    empleado = new Empleado
                                    {
                                        Id = Convert.ToInt32(dr["IdEmpleado"]),
                                        Nombre = dr["Nombre"].ToString(),
                                        Apellido = dr["Apellido"].ToString(),
                                    },
                                    Habilitada= Convert.ToBoolean(dr["Habilitada"]),
                                    Eliminado = Convert.ToBoolean(dr["Eliminado"]),
                                };
                                planillas.Add(planilla);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }

            return planillas;
        }
    }
}
