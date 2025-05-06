using ApiRest.Modelo;
using Microsoft.Extensions.Logging;
using System.Data;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiRest.Repositorio
{
    public class UsuariosSQLServer: IUsuariosSQLServer
    {
        private string CadenaConexion;
        private readonly ILogger<UsuariosSQLServer> log;

        public UsuariosSQLServer(AccesoDatos cadenaConexion, ILogger<UsuariosSQLServer> L)
        {
            CadenaConexion = cadenaConexion.CadenaConexionSQL;
            this.log = L;
        }
        private SqlConnection conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public async Task<UsuarioAPI> DameUsuario(LoginAPI login) {

            SqlConnection sqlConexion = conexion();
            SqlCommand Comm = null;
            UsuarioAPI u = null;
            try
            {
                sqlConexion.Open();
                Comm = sqlConexion.CreateCommand();
                Comm.CommandText = "dbo.UsuarioAPI_Obtener";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@UsuarioAPI", SqlDbType.VarChar, 500).Value = login.usuarioAPI;
                Comm.Parameters.Add("@PassAPI", SqlDbType.VarChar, 500).Value = login.passAPI;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                if (reader.Read())
                {
                    u = new UsuarioAPI
                    {
                        Usuario = reader["UsuarioApi"].ToString(),
                        Email = reader["EmailUsuario"].ToString(),


                    };




                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al logearse" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConexion.Close();
                sqlConexion.Dispose();
            }

            return u;





        }
    }
}
