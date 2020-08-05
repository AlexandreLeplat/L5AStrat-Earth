using System.Linq;
using System.Security.Claims;
using API.Database;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Classe CAMPAIGNS : gère les informations liées aux campagnes
    [Route("campaigns")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        // GET campaigns permet de récupérer la liste des campagnes
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult Get()
        {
            return NotFound();
        }

        // GET campaigns/current permet de récupérer la campagnes courante
        [HttpGet("current")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCurrent()
        {
            using (var dal = new DAL())
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();

                // On récupère la campagne associée au compte joueur courant
                var id = long.Parse(claim.Value);
                var campaign = (from c in dal.Campaigns
                                join p in dal.Players on c.Id equals p.CampaignId
                             where p.Id == id
                             select c).FirstOrDefault();
                if (campaign == null) return NotFound();

                return Ok(campaign);
            }
        }

        // GET campaigns/current/players permet de récupérer la liste des joueurs de la campagne courante
        [HttpGet("current/players")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCurrentCampaignPlayers()
        {
            using (var dal = new DAL())
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();

                // On récupère les joueurs ayant le même idCampaign que le joueur courant
                var id = long.Parse(claim.Value);
                var players = (from c in dal.Players
                               join p in dal.Players on c.CampaignId equals p.CampaignId
                               where c.Id == id
                               select p).ToList();
                if (players == null) return NotFound();

                // On filtre les informations pour n'afficher que ce que le joueur a le droit de voir
                players = players.Select(p => new Player()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Color = p.Color,
                    UserId = p.UserId,
                    CampaignId = p.CampaignId
                }).ToList();

                return Ok(players);
            }
        }

    }
}
