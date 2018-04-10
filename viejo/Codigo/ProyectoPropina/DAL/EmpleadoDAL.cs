﻿using ET;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmpleadoDAL
    {
        public Empleado obtener(int id)
        {
            Empleado empleado = null;
            string cadenaSelectUsuario = "SELECT * FROM USUARIO u, EMPLEADO e WHERE u.Id=e.UsuarioId AND (u.Tipo='EMPLEADO' OR u.Tipo='ADMINISTRADOR') AND u.Id = @id";
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
                                empleado = new Empleado
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    NombreUsuario = dr["NombreUsuario"].ToString(),
                                    Contrasena = dr["Contrasenia"].ToString(),
                                    Habilitado = Convert.ToBoolean(dr["Habilitado"]),
                                    Documento = dr["Documento"].ToString(),
                                    CorreoElectronico = dr["CorreoElectronico"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Tipo = dr["Tipo"].ToString(),
                                    NumeroEmpleado = Convert.ToInt32(dr["NumeroEmpleado"]),
                                    AusenciasAnteriores = Convert.ToDouble(dr["Ausencias"]),
                                    AusenciasAnterioresAsamblea = Convert.ToDouble(dr["AusenciasAsamblea"]),
                                    Cargo= new Cargo { Id = Convert.ToInt32(dr["CargoId"]), },
                                    RepartoEmpleado = new RepartoEmpleado(),
                                    Eventual = Convert.ToBoolean(dr["Eventual"]),
                                    DiasTrabajadosEventuales = Convert.ToDouble(dr["DiasTrabajadosEventuales"]),
                                    FechaIngreso=Convert.ToDateTime(dr["FechaIngreso"])
                                };
                            }
                        }
                        //Ver que si el cliente tuviera datos en la tabla cliente habria que hacer otra lectura
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ProyectoException("Error: " + ex.Message);
            }

            return empleado;
        }       
        public List<Empleado> obtenerTodos()
        {
            List<Empleado> empleados = new List<Empleado>();
            string cadenaSelectUsuarios = "SELECT * FROM USUARIO u, EMPLEADO e WHERE u.Id=e.UsuarioId AND (u.Tipo='EMPLEADO' OR u.Tipo='ADMINISTRADOR') ORDER BY Apellido ASC";
            try
            {
                using (SqlConnection con = new SqlConnection(Utilidades.conn))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(cadenaSelectUsuarios, con))
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
                                    NombreUsuario = dr["NombreUsuario"].ToString(),
                                    Contrasena = dr["Contrasenia"].ToString(),
                                    Habilitado = Convert.ToBoolean(dr["Habilitado"]),
                                    Documento = dr["Documento"].ToString(),
                                    CorreoElectronico = dr["CorreoElectronico"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Tipo = dr["Tipo"].ToString(),
                                    NumeroEmpleado = Convert.ToInt32(dr["NumeroEmpleado"]),
                                    AusenciasAnteriores = Convert.ToDouble(dr["Ausencias"]),
                                    AusenciasAnterioresAsamblea = Convert.ToDouble(dr["AusenciasAsamblea"]),
                                    Cargo = new Cargo { Id = Convert.ToInt32(dr["CargoId"]), },
                                    RepartoEmpleado = new RepartoEmpleado(),
                                    Eventual = Convert.ToBoolean(dr["Eventual"]),
                                    DiasTrabajadosEventuales = Convert.ToDouble(dr["DiasTrabajadosEventuales"]),
                                    FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"])
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
        //public Empleado obtener(string email)
        //{
        //    Cliente cli = null;
        //    string cadenaSelectUsuario = "SELECT b.Nombre as NombreBarrio, d.Nombre as NombreDepto, * FROM Usuario u, Barrio b, Departamento d WHERE u.BarrioId=b.Id AND b.DepartamentoId=d.Id AND u.Tipo='CLIENTE' AND u.Email = @email";
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(Utilidades.conn))
        //        {
        //            using (SqlCommand cmd = new SqlCommand(cadenaSelectUsuario, con))
        //            {
        //                cmd.Parameters.AddWithValue("@email", email);
        //                con.Open();
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    dr.Read();
        //                    if (dr.HasRows)
        //                    {
        //                        cli = new Cliente
        //                        {
        //                            Id = Convert.ToInt32(dr["Id"]),
        //                            Nombre = dr["Nombre"].ToString(),
        //                            Apellido = dr["Apellido"].ToString(),
        //                            NombreUsuario = dr["NombreUsuario"].ToString(),
        //                            Contrasena = dr["Contrasenia"].ToString(),
        //                            UltimaModificacionContrasena = Convert.ToDateTime(dr["UltimaModificacionContrasenia"]),
        //                            Habilitado = Convert.ToBoolean(dr["Habilitado"]),
        //                            CorreoElectronico = dr["Email"].ToString(),
        //                            Telefono = dr["Telefono"].ToString(),
        //                            Direccion = dr["Direccion"].ToString(),
        //                            FechaAlta = Convert.ToDateTime(dr["FechaAlta"]),
        //                            Barrio = new Barrio
        //                            {
        //                                Id = Convert.ToInt32(dr["BarrioId"]),
        //                                Nombre = dr["NombreBarrio"].ToString(),
        //                                Departamento = new Departamento { Nombre = dr["NombreDepto"].ToString() }
        //                            },
        //                            Tipo = dr["Tipo"].ToString(),
        //                            Imagen = dr["Imagen"].ToString()
        //                        };
        //                    }
        //                }
        //                //Ver que si el cliente tuviera datos en la tabla cliente habria que hacer otra lectura
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ProyectoException("Error: " + ex.Message);
        //    }

        //    return cli;
        //}
        //public void alta(Empleado empleado)
        //{
        //    string cadenaInsertUsuario = @"INSERT INTO Usuario VALUES(@nom, @ape, @nomUsu,@pass, @ultModif, @habilitado, @email, @tel, @dir, @fechaAlta, @tipo, @barrio, @img, @token, @fechaToken); 
        //                                    SELECT CAST(Scope_Identity() AS INT);";
        //    string cadenaInsertEmpleado = "INSERT INTO Cliente VALUES(@idUsu);";
        //    int idClienteGenerado = 0;

        //    SqlTransaction trn = null;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(Utilidades.conn))
        //        {
        //            using (SqlCommand cmd = new SqlCommand(cadenaInsertUsuario, con))
        //            {
        //                cmd.Parameters.AddWithValue("@nom", cli.Nombre);
        //                cmd.Parameters.AddWithValue("@ape", cli.Apellido);
        //                cmd.Parameters.AddWithValue("@nomUsu", cli.NombreUsuario);
        //                cmd.Parameters.AddWithValue("@pass", Utilidades.calcularMD5Hash(cli.Contrasena));
        //                cmd.Parameters.AddWithValue("@ultModif", DateTime.Now);
        //                cmd.Parameters.AddWithValue("@habilitado", cli.Habilitado);
        //                cmd.Parameters.AddWithValue("@email", cli.CorreoElectronico);
        //                cmd.Parameters.AddWithValue("@tel", cli.Telefono);
        //                cmd.Parameters.AddWithValue("@dir", cli.Direccion);
        //                cmd.Parameters.AddWithValue("@fechaAlta", DateTime.Now);
        //                cmd.Parameters.AddWithValue("@barrio", cli.Barrio.Id);
        //                cmd.Parameters.AddWithValue("@tipo", "CLIENTE");
        //                cmd.Parameters.AddWithValue("@token", DBNull.Value);
        //                cmd.Parameters.AddWithValue("@fechaToken", Convert.ToDateTime("1900 - 01 - 01"));

        //                cmd.Parameters.AddWithValue("@img", cli.NombreUsuario.ToUpper().Replace(" ", "") + ".jpg");

        //                con.Open();
        //                trn = con.BeginTransaction();
        //                cmd.Transaction = trn;

        //                idClienteGenerado = (int)cmd.ExecuteScalar();
        //                cmd.Parameters.Clear();

        //                cmd.CommandText = cadenaInsertEmpleado;

        //                cmd.Parameters.AddWithValue("@idUsu", idClienteGenerado);

        //                cmd.ExecuteNonQuery();

        //                trn.Commit();
        //                trn.Dispose();

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ProyectoException("Error: " + ex.Message);
        //    }
        //}
        //public void actualizarCliente(Cliente cli)
        //{
        //    string cadenaUpdateUsuario = @"UPDATE Usuario SET Nombre=@nom, Apellido=@ape, Telefono=@tel, Direccion=@dir, BarrioId=@barrio
        //                                    WHERE Id=@id;";
        //    //string cadenaUpdateCliente = "UPDATE Cliente SET datos... WHERE UduarioId=@id;"; //PARA CUANDO HAYA MAS DATOS

        //    SqlTransaction trn = null;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(Utilidades.conn))
        //        {
        //            using (SqlCommand cmd = new SqlCommand(cadenaUpdateUsuario, con))
        //            {
        //                cmd.Parameters.AddWithValue("@id", cli.Id);
        //                cmd.Parameters.AddWithValue("@nom", cli.Nombre);
        //                cmd.Parameters.AddWithValue("@ape", cli.Apellido);
        //                cmd.Parameters.AddWithValue("@tel", cli.Telefono);
        //                cmd.Parameters.AddWithValue("@dir", cli.Direccion);
        //                cmd.Parameters.AddWithValue("@barrio", cli.Barrio.Id);

        //                con.Open();
        //                trn = con.BeginTransaction();
        //                cmd.Transaction = trn;

        //                cmd.ExecuteNonQuery();

        //                //cmd.Parameters.Clear(); //PARA CUANDO HAYA MAS DATOS

        //                //cmd.CommandText = cadenaUpdateCliente;

        //                //cmd.Parameters.AddWithValue("@id", cli.Id);
        //                //mas datos...

        //                //cmd.ExecuteNonQuery();

        //                trn.Commit();
        //                trn.Dispose();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ProyectoException("Error: " + ex.Message);
        //    }
        //}
        //Por el momento se repite codigo... ver si interesa diferenciar el ingreso de administrador de cliente
        //public Cliente ingresarCliente(string NombreUsu, string pass)
        //{
        //    Cliente cli = null;
        //    string cadenaSelectUsuario = "SELECT * FROM Usuario WHERE NombreUsuario=@nomUsu AND Contrasenia=@pass AND Habilitado=1 AND Tipo='CLIENTE'";
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(Utilidades.conn))
        //        {
        //            using (SqlCommand cmd = new SqlCommand(cadenaSelectUsuario, con))
        //            {
        //                cmd.Parameters.AddWithValue("@nomUsu", NombreUsu);
        //                cmd.Parameters.AddWithValue("@pass", Utilidades.calcularMD5Hash(pass));
        //                con.Open();
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    dr.Read();
        //                    if (dr.HasRows)
        //                    {
        //                        cli = new Cliente
        //                        {
        //                            Id = Convert.ToInt32(dr["Id"]),
        //                            Nombre = dr["Nombre"].ToString(),
        //                            Apellido = dr["Apellido"].ToString(),
        //                            NombreUsuario = dr["NombreUsuario"].ToString(),
        //                            Contrasena = dr["Contrasenia"].ToString(),
        //                            UltimaModificacionContrasena = Convert.ToDateTime(dr["UltimaModificacionContrasenia"]),
        //                            Habilitado = Convert.ToBoolean(dr["Habilitado"]),
        //                            CorreoElectronico = dr["Email"].ToString(),
        //                            Telefono = dr["Telefono"].ToString(),
        //                            Direccion = dr["Direccion"].ToString(),
        //                            FechaAlta = Convert.ToDateTime(dr["FechaAlta"]),
        //                            Barrio = new Barrio { Id = Convert.ToInt32(dr["BarrioId"]) },
        //                            Tipo = dr["Tipo"].ToString()
        //                        };
        //                    }
        //                }
        //                //Ver que si el cliente tuviera datos en la tabla cliente habria que hacer otra lectura
        //                //Cargar demás datos del barrio sería lo mejor también
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ProyectoException("Error: " + ex.Message);
        //    }

        //    return cli;
        //}
    }
}
