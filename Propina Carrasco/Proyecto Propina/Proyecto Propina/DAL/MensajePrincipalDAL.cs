using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ET;
using System.Data.SqlClient;

namespace DAL
{
    public class MensajePrincipalDAL
    {
        public List<MensajePrincipal> obtenerTodos()
        {
            List<MensajePrincipal> mensajes = new List<MensajePrincipal>();
            string cadenaSelect = "SELECT * FROM MENSAJE_PRINCIPAL;";
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
                                MensajePrincipal mensaje = new MensajePrincipal
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Texto = dr["Texto"].ToString(),
                                    Fecha = Convert.ToDateTime(dr["Fecha"]),
                                    Titulo = dr["Titulo"].ToString(),
                                };
                                mensajes.Add(mensaje);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }

            return mensajes;
        }

        public void altaMensaje(MensajePrincipal mensaje)
        {
            string cadenaInsert = @"INSERT INTO MENSAJE_PRINCIPAL VALUES(@texto, @titulo, @fecha);";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    using (SqlCommand cmd = new SqlCommand(cadenaInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@texto", mensaje.Texto);
                        cmd.Parameters.AddWithValue("@titulo", mensaje.Titulo);
                        cmd.Parameters.AddWithValue("@fecha", mensaje.Fecha);

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

        public void eliminarMensaje(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("Delete MENSAJE_PRINCIPAL WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@id", id);
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
