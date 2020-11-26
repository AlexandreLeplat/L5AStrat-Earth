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
        public ActionResult GetPlayers()
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

        // POST players permet de créer un joueur sur une campagne en préparation
        [HttpPost]
        [EnableCors]
        [Authorize]
        public ActionResult Post([FromBody] Player model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Color))
                return BadRequest("Paramètres manquants");

            if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Trim().Length > 25)
                return BadRequest("Nom incorrect");

            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var userId = long.Parse(claim.Value);

                // On vérifie l'existence de l'utilisateur
                if (_dal.Users.Any(u => u.Id == userId))
                    return Unauthorized();

                // On récupère la campagne ciblée pour vérifier les prérequis
                var campaign = _dal.Campaigns.FirstOrDefault(c => c.Id == model.CampaignId);
                if (campaign == null) return NotFound();

                if (campaign.Status != CampaignStatus.Preparation)
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, "Le statut de la campagne ne permet pas cette action");
                var campaignPlayers = _dal.Players.Where(p => p.CampaignId == campaign.Id && !p.IsAdmin).ToList();
                
                if (campaignPlayers.Count() >= 4) // TODO : gérer dynamiquement le max de joueurs
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, "La campagne ne peut plus accepter de joueur supplémentaire");

                if (campaignPlayers.Exists(p => p.Color == model.Color || p.Name.ToUpperInvariant() == model.Name.ToUpperInvariant().Trim()))
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, "Le nom ou la couleur a déjà été pris sur cette campagne");

                if (campaignPlayers.Exists(p => p.UserId == userId))
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, "Vous avez déjà créé un joueur sur cette campagne");

                Player player = new Player()
                {
                    Name = model.Name.Trim(),
                    Color = model.Color,
                    CampaignId = campaign.Id,
                    UserId = model.UserId,
                    Assets = new Dictionary<string, Dictionary<string, string>>()
                };

                _dal.Players.Add(player);
                _dal.SaveChanges();

                return Ok(model);
            }
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
