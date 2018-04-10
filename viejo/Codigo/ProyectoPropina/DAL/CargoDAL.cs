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
        public static List<Cargo> obtenerTodos()
        {
            List<Cargo> cargos = new List<Cargo>();
            string cadenaSelectCargos = "SELECT * FROM CARGO ORDER BY ID";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(cadenaSelectCargos, con))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Cargo cargo = new Cargo
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Puntaje = Convert.ToDouble(dr["Puntaje"]),
                                    Tipo = dr["Tipo"].ToString(),
                                    Area = dr["Area"].ToString()
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
