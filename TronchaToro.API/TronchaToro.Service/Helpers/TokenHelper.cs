using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TronchaToro.Service.Models;
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
        public object CrearToken(UserModel request) //List<string> roles
        {
            try
            {
                //CLAIMS
                var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.NameId, request.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                SecurityTokenDescriptor tokenDescriptcion = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(30),
                    SigningCredentials = credentials
                };

                JwtSecurityToken tokenManejador = new JwtSecurityToken(
                    _configuration["Tokens:Issuer"],
                    _configuration["Tokens:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddDays(99),
                    signingCredentials: credentials
                );

                var results = new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(tokenManejador),
                    expiration = tokenManejador.ValidTo,
                    request
                };

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string cifrarMD5(string password)
        {
            MD5CryptoServiceProvider encriptador = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(password);
            bs = encriptador.ComputeHash(bs);

            string passHash = null;

            foreach (byte b in bs)
            {
                passHash += b.ToString("x2").ToLower();
            }

            string clave_cifrada = passHash;
            return clave_cifrada;
        }
    }
}
