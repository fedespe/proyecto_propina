using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using System.Data.SqlClient;

namespace DAL
{
    public class CargoDAL
    {
        public List<Cargo> obtenerTodos()
        {
            List<Cargo> cargos = new List<Cargo>();
            string cadenaSelect = "SELECT * FROM CARGO;";
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
                                Cargo cargo = new Cargo
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Puntaje = Convert.ToDouble(dr["Puntaje"])
                                };
                                cargos.Add(cargo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }

            return cargos;
        }
    }
}
