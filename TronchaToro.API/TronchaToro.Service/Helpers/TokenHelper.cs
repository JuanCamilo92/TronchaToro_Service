using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TronchaToro.Service.Models.Requests;

namespace TronchaToro.Service.Helpers
{
    public class TokenHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;

        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CrearToken(LoginRequest request) //List<string> roles
        {
            try
            {
                //CLAIMS
                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.NameId, request.Identificacion)
                };

                //if (roles != null)
                //{
                //    foreach (var rol in roles)
                //        claims.Add(new Claim(ClaimTypes.Role, rol));
                //}

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptcion = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(30),
                    SigningCredentials = credenciales
                };

                var tokenManejador = new JwtSecurityTokenHandler();
                var token = tokenManejador.CreateToken(tokenDescriptcion);
                var tokenFinal = tokenManejador.WriteToken(token);

                return tokenFinal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
