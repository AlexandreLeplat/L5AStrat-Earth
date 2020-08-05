using Entities;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Security
{
    public class JWTHelper
    {
        private readonly string _secret;
        private readonly string _tokenLifeTime;

        public JWTHelper(IConfiguration config)
        {
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            _tokenLifeTime = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
        }

        // Génère un token d'authentification pour un joueur
        public Token GenerateSecurityToken(Player player)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var expiration = DateTime.UtcNow.AddMinutes(double.Parse(_tokenLifeTime));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, player.Id.ToString()),
                    new Claim(ClaimTypes.Name, player.Name)
                }),
                Expires = expiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new Token()
            {
                Jwt = tokenHandler.WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
