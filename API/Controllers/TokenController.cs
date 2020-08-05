using API.Database;
using API.Models;
using API.Security;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;

namespace API.Controllers
{
    // Classe TOKEN : gère l'authentification à l'API via un token JWT
    [Route("token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        // POST token : Permet de se loguer avec un nom d'utilisateur et mot de passe, en récupérant un token JWT
        [HttpPost]
        [EnableCors]
        public ActionResult Post([FromBody] TokenInputModel input)
        {
            var jwt = new JWTHelper(_config);
            /*
             * Admin / admin
             * Dragon / Togashi
             * Grue / Ikebana
             * Lion / Bagarre
             * Scorpion / J3sa!5Pr0tég3Rme$SecR3ts
            */

            // Ligne de test pour générer des passwords hashés
            // return Ok(BCrypt.Net.BCrypt.HashPassword(input.Password));

            using (var dal = new DAL())
            {
                // On récupère l'utilisateur en fonction de son nom
                var user = dal.Users.FirstOrDefault(u => u.Name == input.Name);
                if (user == null)
                {
                    return Unauthorized();
                }

                // Si le mot de passe correspond, on récupère un token signé
                if (BCrypt.Net.BCrypt.Verify(input.Password, user.Password))
                {
                    var mainPlayer = dal.Players.FirstOrDefault(p => p.UserId == user.Id && p.IsCurrentPlayer);
                    if (mainPlayer == null) return NotFound();

                    var token = jwt.GenerateSecurityToken(mainPlayer);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        // GET token : Permet de renouveler un token existant pour repousser l'expiration
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult Get()
        {
            var jwt = new JWTHelper(_config);
            var id = long.Parse(User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault().Value);
            var name = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            var token = jwt.GenerateSecurityToken(new Player() { Id = id, Name = name });
            return Ok(token);
        }
    }
}
