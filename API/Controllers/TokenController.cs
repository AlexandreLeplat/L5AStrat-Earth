using Entities.Database;
using HostApp.Models;
using HostApp.Security;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using Entities.Enums;

namespace HostApp.Controllers
{
    // Classe TOKEN : gère l'authentification à l'API via un token JWT
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;

        private readonly DAL _dal;

        public TokenController(IConfiguration config, DAL dal)
        {
            _config = config;
            _dal = dal;
        }

        // POST token : Permet de se loguer avec un nom d'utilisateur et mot de passe, en récupérant un token JWT
        [HttpPost]
        [EnableCors]
        public ActionResult Post([FromBody] TokenInputModel input)
        {
            var jwt = new JWTHelper(_config);

            // Ligne de test pour générer des passwords hashés
            // return Ok(BCrypt.Net.BCrypt.HashPassword(input.Password));

            using (_dal)
            {
                // On récupère l'utilisateur en fonction de son nom
                var user = _dal.Users.FirstOrDefault(u => u.Name == input.Name);
                if (user == null)
                {
                    return Unauthorized();
                }

                // Si le mot de passe correspond, on récupère un token signé
                if (BCrypt.Net.BCrypt.Verify(input.Password, user.Password))
                {
                    var mainPlayer = _dal.Players.FirstOrDefault(p => p.UserId == user.Id && p.IsCurrentPlayer);
                    if (mainPlayer == null) mainPlayer = new Player() { Name = string.Empty, UserId = user.Id };

                    var token = jwt.GenerateSecurityToken(mainPlayer);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        // GET token : Permet d'échanger son token contre celui d'un autre joueur du même utilisateur
        [HttpGet("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult GetSwitch(long? id)
        {
            var jwt = new JWTHelper(_config);
            var idUser = long.Parse(User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault().Value);

            using (_dal)
            {
                var mainPlayer = _dal.Players.FirstOrDefault(p => p.UserId == idUser && p.IsCurrentPlayer);
                if (mainPlayer != null) mainPlayer.IsCurrentPlayer = false;

                var selectedPlayer = _dal.Players.FirstOrDefault(p => p.UserId == idUser && p.Id == id.Value);
                if (selectedPlayer == null) selectedPlayer = new Player() { Name = string.Empty, UserId = idUser };
                else
                {
                    selectedPlayer.IsCurrentPlayer = true;
                    if (selectedPlayer.Status == PlayerStatus.Ready) selectedPlayer.Status = PlayerStatus.Playing;
                }

                _dal.SaveChanges();

                var token = jwt.GenerateSecurityToken(selectedPlayer);
                return Ok(token);
            }
        }
    }
}
