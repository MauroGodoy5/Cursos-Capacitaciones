using ApiRest.Modelo;
using System.Threading.Tasks;

namespace ApiRest.Repositorio
{
    public interface IUsuariosSQLServer
    {
        Task<UsuarioAPI> DameUsuario(LoginAPI login);
    }
}
