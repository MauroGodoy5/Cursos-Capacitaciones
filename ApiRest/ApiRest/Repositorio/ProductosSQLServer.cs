using ApiRest.Modelo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.Repositorio
{
    public class ProductosSQLServer : IProductosEnMemoria
    {
        private string CadenaConexion;
        private readonly ILogger<ProductosSQLServer> log;

        public ProductosSQLServer(AccesoDatos cadenaConexion, ILogger<ProductosSQLServer> L)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = L;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }
        public async Task BorrarProductoAsincrono(string SKU)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.Productos_Borrar";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = SKU;
                await Comm.ExecuteNonQueryAsync();
                


            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al borrar un producto" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            await Task.CompletedTask;

        }

        public async Task CrearProductoAsincrono(Producto p)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.ProductosAlta";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@Nombre", SqlDbType.VarChar, 500).Value = p.Nombre;
                Comm.Parameters.Add("@Descripcion", SqlDbType.VarChar, 5000).Value = p.Descripcion;
                Comm.Parameters.Add("@Precio", SqlDbType.Float).Value = p.Precio;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = p.SKU;
                await Comm.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al dar de alta" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            await Task.CompletedTask;
        }

        public async Task<Producto> DameProductoAsincrono(string SKU) 
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            Producto p = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.Productos_Obtener";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = SKU;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                if (reader.Read())
                {
                    p = new Producto
                    {
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        Precio = Convert.ToDouble(reader["Precio"].ToString()),
                        SKU = reader["SKU"].ToString()


                    };




                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al dar de alta" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return p;
        }

        public async Task <IEnumerable<Producto>> DameProductosAsincrono()
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            List<Producto> productos = new List<Producto>();
            Producto p = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.Productos_Obtener";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                while (reader.Read())
                {
                    p = new Producto
                    {
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        Precio = Convert.ToDouble(reader["Precio"].ToString()),
                        SKU = reader["SKU"].ToString()


                    };
                    productos.Add(p);



                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al dar de alta" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return productos;
        }

        public async Task ModificarProductoAsincrono(Producto p)
        {
            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.Productos_Modificar";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@Nombre", SqlDbType.VarChar, 500).Value = p.Nombre;
                Comm.Parameters.Add("@Descripcion", SqlDbType.VarChar, 5000).Value = p.Descripcion;
                Comm.Parameters.Add("@Precio", SqlDbType.Float).Value = p.Precio;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = p.SKU;
                await Comm.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al modificar un producto" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }
            await Task.CompletedTask;
        }
    }
}
