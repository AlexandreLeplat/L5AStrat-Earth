using System.Linq;
using System.Security.Claims;
using API.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Classe PLAYERS : gère les informations des joueurs
    [Route("players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        // GET players permet de récupérer la liste des joueurs de l'utilisateur
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult Get()
        {
            using (var dal = new DAL())
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();

                // On récupère les joueurs ayant le même idUser que le joueur courant
                var id = long.Parse(claim.Value);
                var players = (from c in dal.Players
                              join p in dal.Players on c.UserId equals p.UserId
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
            using (var dal = new DAL())
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();

                // On récupère le joueur courant
                var id = long.Parse(claim.Value);
                var player = dal.Players.FirstOrDefault(p => p.Id == id);
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
        public ActionResult Put(int id, [FromBody] string value)
        {
            return NotFound();
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
