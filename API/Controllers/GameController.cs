using System.Linq;
using System.Security.Claims;
using Entities.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HostApp.Controllers
{
    // Classe GAMES : gère les informations liées à un système de jeu
    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly DAL _dal;

        public GameController(DAL dal)
        {
            _dal = dal;
        }
        
        // GET games permet de récupérer la liste des systèmes de jeu
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult Get()
        {
            using (_dal)
            {
                var games = _dal.Games.ToList();
                return Ok(games);
            }
        }

        // GET games/current permet de récupérer le système de jeu courant
        [HttpGet("current")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCurrent()
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();

                // On récupère le système de jeu de la campagne associée au compte joueur courant
                var id = long.Parse(claim.Value);
                var game = (from g in _dal.Games 
                                join c in _dal.Campaigns on g.Id equals c.GameId 
                                join p in _dal.Players on c.Id equals p.CampaignId
                             where p.Id == id
                             select g).FirstOrDefault();
                if (game == null) return NotFound();

                return Ok(game);
            }
        }
    }
}
