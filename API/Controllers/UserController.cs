using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Entities.Database;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HostApp.Controllers
{
    // Classe USERS : gère les utilisateurs
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DAL _dal;
        private readonly string _subscriptionKey;

        public UserController(IConfiguration config, DAL dal)
        {
            _dal = dal;
            _subscriptionKey = config.GetSection("AppSettings").GetSection("subscriptionKey").Value;
        }

        // GET users permet de récupérer la liste des utilisateurs de l'application
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult GetUsers()
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var userId = long.Parse(claim.Value);

                var users = (from u in _dal.Users
                             select u).ToList();
                users.ForEach(u => u.Password = string.Empty);

                return Ok(users);
            }
        }

        // GET users/current permet de récupérer l'utilisateur courant
        [HttpGet("current")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCurrent()
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var userId = long.Parse(claim.Value);

                var user = _dal.Users.FirstOrDefault(p => p.Id == userId);
                if (user == null) return NotFound();
                user.Password = string.Empty;

                return Ok(user);
            }
        }

        // GET users/{id} permet de récupérer un utilisateur défini
        [HttpGet("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult Get(long? id)
        {
            var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
            if (claim == null) return Unauthorized();
            var userId = long.Parse(claim.Value);

            var user = _dal.Users.FirstOrDefault(p => p.Id == id.Value);
            if (user == null) return NotFound();
            user.Password = string.Empty;

            return Ok(user);
        }

        // POST users permet de créer un utilisateur
        [HttpPost]
        [EnableCors]
        public ActionResult Post([FromBody] User model, string subscriptionKey = "")
        {
            if (model == null || string.IsNullOrWhiteSpace(subscriptionKey))
                return BadRequest("Paramètres manquants");

            if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Trim().Length < 2 || model.Name.Trim().Length > 25)
                return BadRequest("Nom incorrect");

            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Trim().Length < 5 || model.Password.Length > 25)
                return BadRequest("Mot de passe incorrect");

            using (_dal)
            {
                if (subscriptionKey.ToUpperInvariant().Trim() != _subscriptionKey) return Unauthorized();

                // On vérifie l'unicité de l'utilisateur
                if (_dal.Users.Any(u => u.Name == model.Name.Trim()))
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, "Nom d'utilisateur déjà existant");

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                User user = new User()
                {
                    Name = model.Name.Trim(),
                    Password = hashedPassword,
                    Role = UserRole.None
                };

                _dal.Users.Add(user);
                _dal.SaveChanges();
                user.Password = string.Empty;

                return Ok(user);
            }
        }
    }
}
