﻿using ET;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioDAL
    {
        //public void actualizarContrasena(int id, string contrasenaNueva)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(Utilidades.conn))
        //        {
        //            con.Open();

        //            SqlCommand cmd = new SqlCommand("UPDATE Usuario SET Contrasenia = @pass WHERE id = @id", con);

        //            cmd.Parameters.AddWithValue("@pass", Utilidades.calcularMD5Hash(contrasenaNueva));
        //            cmd.Parameters.AddWithValue("@id", id);

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ProyectoException("Error: " + ex.Message);
        //    }
        //}

        //public void habilitarUsuario(int id)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(Utilidades.conn))
        //        {
        //            con.Open();

        //            SqlCommand cmd = new SqlCommand("Update Usuario SET Habilitado = 1 WHERE id = @id", con);
        //            cmd.Parameters.AddWithValue("@id", id);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ProyectoException("Error: " + ex.Message);
        //    }
        //}
        //public void deshabilitarUsuario(int id)
        //{
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(Utilidades.conn))
        //        {
        //            con.Open();

        //            SqlCommand cmd = new SqlCommand("Update Usuario SET Habilitado = 0 WHERE id = @id", con);
        //            cmd.Parameters.AddWithValue("@id", id);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ProyectoException("Error: " + ex.Message);
        //    }
        //}
        public bool existeNombreUsuario(string nombreUsuario)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @nomUsu", con);
                    cmd.Parameters.AddWithValue("@nomUsu", nombreUsuario);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        return dr.HasRows;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }
        }
        public bool existeCorreoElectronico(string correo)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE CorreoElectronico = @correo", con);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        return dr.HasRows;
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
