using System.Linq;
using System.Security.Claims;
using Entities.Database;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HostApp.Controllers
{
    // Classe PLAYERS : gère les informations des joueurs
    [Route("api/players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly DAL _dal;

        public PlayerController(DAL dal)
        {
            _dal = dal;
        }

        // GET players permet de récupérer la liste des joueurs de l'utilisateur
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult Get()
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();

                // On récupère les joueurs ayant le même idUser que le joueur courant
                var id = long.Parse(claim.Value);
                var players = (from c in _dal.Players
                              join p in _dal.Players on c.UserId equals p.UserId
                              where c.Id == id
                              select p).ToList();
                if (players == null) return NotFound();

                return Ok(players);
            }
        }

        // GET players/current permet de récupérer le joueur courant
        [HttpGet("current")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCurrent()
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();

                // On récupère le joueur courant
                var id = long.Parse(claim.Value);
                var player = _dal.Players.FirstOrDefault(p => p.Id == id);
                if (player == null) return NotFound();

                return Ok(player);
            }
        }

        // GET players/{id} permet de récupérer un joueur défini
        [HttpGet("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult Get(long? id)
        {
            return NotFound();
        }

        // POST players permet de créer un joueur
        [HttpPost]
        [EnableCors]
        [Authorize]
        public ActionResult Post([FromBody] string value)
        {
            return NotFound();
        }

        // PUT players permet de mettre à jour un joueur
        [HttpPut("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult Put(long? id, [FromBody] Player model)
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null || long.Parse(claim.Value) != id) return Unauthorized();

                // On récupère le joueur courant
                var player = _dal.Players.FirstOrDefault(p => p.Id == id);
                if (player == null) return NotFound();

                player.HasNewMap = model.HasNewMap;
                _dal.SaveChanges();

                return Ok(player);
            }
        }

        // DELETE players permet de supprimer un joueur
        [HttpDelete("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult Delete(int id)
        {
            return NotFound();
        }
    }
}
