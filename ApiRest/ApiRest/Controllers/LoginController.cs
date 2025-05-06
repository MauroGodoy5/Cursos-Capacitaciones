using ApiRest.DTO;
using ApiRest.Modelo;
using ApiRest.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Controllers
{
    [Route("Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<LoginController> log;
        private readonly IUsuariosSQLServer repositorio;


        public LoginController(IConfiguration configuration, ILogger<LoginController> l, IUsuariosSQLServer r)
        {
            this.configuration = configuration;
            this.log = l;
            this.repositorio = r;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UsuarioDTO>> Login(LoginAPI usuarioLogin) {

            UsuarioAPI Usuario = null;
            Usuario = await AuntenticarUsuarioAsync(usuarioLogin);
            if (Usuario == null)
                throw new Exception("Creedenciales Invalidas");

            else
                Usuario = GenerarTokenJWT(Usuario);


            return Usuario.convertirDTO();
        
        
        
        
        
        }

        private async Task<UsuarioAPI> AuntenticarUsuarioAsync(LoginAPI usuarioLogin)
        {
            UsuarioAPI usuarioAPI = await repositorio.DameUsuario(usuarioLogin);

            return usuarioAPI;
        }


        private UsuarioAPI GenerarTokenJWT(UsuarioAPI usuarioInfo)
        {
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:ClaveSecreta"]));

            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var _header = new JwtHeader(_signingCredentials);

            var _Claims = new[] {

                new Claim("usuario", usuarioInfo.Usuario),
                new Claim("email", usuarioInfo.Email),
                new Claim(JwtRegisteredClaimNames.Email, usuarioInfo.Email)
            };

            var _payload = new JwtPayload(

                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: _Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1)
                );

            var _Token = new JwtSecurityToken(
                _header,
                _payload
                );
            usuarioInfo.Token = new JwtSecurityTokenHandler().WriteToken(_Token);

            return usuarioInfo;
        }
    }
}
