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
            string cadenaInsertPregunta = @"INSERT INTO Planilla VALUES(@texto, @fecha);";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaInsertPregunta, con))
                    {
                        cmd.Parameters.AddWithValue("@texto", pla.Texto);
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);

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

        public List<Planilla> obtenerTodos()
        {
            List<Planilla> planillas = new List<Planilla>();
            string cadenaSelectPregunta = "SELECT p.*,u.Nombre, u.Apellido  FROM PLANILLA p, USUARIO u WHERE p.IdEmpleado= u.Id;";
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
