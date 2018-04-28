using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using System.Data.SqlClient;

namespace DAL
{
    public class RepartoDAL
    {
        public List<Reparto> obtenerTodos()
        {
            List<Reparto> repartos = new List<Reparto>();
            string cadenaSelect = "SELECT * FROM REPARTO;";
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
                                Reparto reparto = new Reparto
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    MontoTotalPesos =Convert.ToInt32(dr["MontoTotalPesos"]),
                                    MontoTotalDolares = Convert.ToInt32(dr["MontoTotalDolares"]),
                                    Fecha = Convert.ToDateTime(dr["Fecha"]),  
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                };
                                repartos.Add(reparto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }

            return repartos;
        }

        public void desactivarReparto(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Update Reparto SET Activo = 0 WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public void activarReparto(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Update Reparto SET Activo = 1 WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public void altaReparto(Reparto reparto)
        {
            string cadenaInsert = @"INSERT INTO Reparto VALUES(@montoPesos, @montoDolares, @fecha, @activo);";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@montoPesos", reparto.MontoTotalPesos);
                        cmd.Parameters.AddWithValue("@montoDolares", reparto.MontoTotalDolares);
                        cmd.Parameters.AddWithValue("@fecha", reparto.Fecha);
                        cmd.Parameters.AddWithValue("@activo", false);

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

        public void actualizarReparto(Reparto reparto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Update Reparto SET MontoTotalPesos = @pesos, MontoTotalDolares = @dolares, Fecha = @fecha WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", reparto.Id);
                    cmd.Parameters.AddWithValue("@pesos", reparto.MontoTotalPesos);
                    cmd.Parameters.AddWithValue("@dolares", reparto.MontoTotalDolares);
                    cmd.Parameters.AddWithValue("@fecha", reparto.Fecha);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }

        public Reparto obtener(int id)
        {
            Reparto reparto = null;
            string cadenaSelect = "SELECT * FROM Reparto WHERE id=@id";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(cadenaSelect, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            if (dr.HasRows)
                            {
                                reparto = new Reparto
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    MontoTotalPesos = Convert.ToInt32(dr["MontoTotalPesos"]),
                                    MontoTotalDolares = Convert.ToInt32(dr["MontoTotalDolares"]),
                                    Fecha = Convert.ToDateTime(dr["Fecha"]),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    RepartosDiarios = new List<RepartoDiario>()
                                };                               
                            }
                        }
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SELECT * FROM REPARTODIARIO WHERE IdReparto=@idReparto;";
                        cmd.Parameters.AddWithValue("@idReparto", id);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                reparto.RepartosDiarios.Add(new RepartoDiario
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    MontoPesosMesas = Convert.ToInt32(dr["MontoPesosMesas"]),
                                    MontoDolaresMesas = Convert.ToInt32(dr["MontoDolaresMesas"]),
                                    MontoPesosOtros = Convert.ToInt32(dr["MontoPesosOtros"]),
                                    MontoDolaresOtros = Convert.ToInt32(dr["MontoDolaresOtros"]),
                                    Fecha = Convert.ToDateTime(dr["Fecha"]),
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

            return reparto;
        }
    }
}
