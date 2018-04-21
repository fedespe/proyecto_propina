using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using System.Data.SqlClient;

namespace DAL
{
    public class RepartoDiarioDAL
    {
        public RepartoDiario obtener(int id)
        {
            RepartoDiario repartoDiario = null;
            string cadenaSelect = "SELECT * FROM RepartoDiario WHERE id=@id";
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
                                repartoDiario = new RepartoDiario
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Reparto = new Reparto() {Id= Convert.ToInt32(dr["IdReparto"]) },
                                    MontoPesosMesas = Convert.ToInt32(dr["MontoPesosMesas"]),
                                    MontoDolaresMesas = Convert.ToInt32(dr["MontoDolaresMesas"]),
                                    MontoPesosOtros = Convert.ToInt32(dr["MontoPesosOtros"]),
                                    MontoDolaresOtros = Convert.ToInt32(dr["MontoDolaresOtros"]),
                                    Fecha = Convert.ToDateTime(dr["Fecha"]),
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

            return repartoDiario;
        }

        public void altaRepartoDiario(RepartoDiario repartoDiario)
        {
            string cadenaInsert = @"INSERT INTO RepartoDiario VALUES(@idReparto, @fecha, @montoPesosMesas,
                                    @montoDolaresMesas, @montoPesosOtros, @montoDolaresOtros);";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@idReparto", repartoDiario.Reparto.Id);
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@montoPesosMesas", repartoDiario.MontoPesosMesas);
                        cmd.Parameters.AddWithValue("@montoDolaresMesas", repartoDiario.MontoDolaresMesas);
                        cmd.Parameters.AddWithValue("@montoPesosOtros", repartoDiario.MontoPesosOtros);
                        cmd.Parameters.AddWithValue("@montoDolaresOtros", repartoDiario.MontoDolaresOtros);

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

        public void actualizarRepartoDiario(RepartoDiario repartoDiario)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Update RepartoDiario SET MontoPesosMesas = @pesosMesas, MontoDolaresMesas = @dolaresMesas, MontoPesosOtros = @pesosOtros, MontoDolaresOtros = @dolaresOtros, Fecha = @fecha WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", repartoDiario.Id);
                    cmd.Parameters.AddWithValue("@pesosMesas", repartoDiario.MontoPesosMesas);
                    cmd.Parameters.AddWithValue("@dolaresMesas", repartoDiario.MontoDolaresMesas);
                    cmd.Parameters.AddWithValue("@pesosOtros", repartoDiario.MontoPesosOtros);
                    cmd.Parameters.AddWithValue("@dolaresOtros", repartoDiario.MontoDolaresOtros);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }
    }
}
