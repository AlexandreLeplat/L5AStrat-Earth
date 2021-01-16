using System;
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
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var userId = long.Parse(claim.Value);

                // On récupère les joueurs ayant le même idUser que le joueur courant
                var id = long.Parse(claim.Value);
                var playersList = (from p in _dal.Players
                                   join c in _dal.Campaigns on p.CampaignId equals c.Id
                                   where p.UserId == userId
                                   select new Tuple<Player, string>(p, c.Name)).ToList();

                var players = playersList.Select(t => new Player()
                {
                    Id = t.Item1.Id,
                    Name = t.Item1.Name,
                    Color = t.Item1.Color,
                    UserId = t.Item1.UserId,
                    CampaignId = t.Item1.CampaignId,
                    CampaignName = t.Item2,
                    IsCurrentPlayer = t.Item1.IsCurrentPlayer,
                    Status = t.Item1.Status
                }).ToList();

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

                // On récupère le joueurà  courant
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
            var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
            if (claim == null) return Unauthorized();

            // On récupère le joueur ciblé
            var idCurrent = long.Parse(claim.Value);
            var player = _dal.Players.FirstOrDefault(p => p.Id == id);
            if (player == null) return NotFound();

            if (player.Id != idCurrent)
            {
                player.Assets = new Dictionary<string, Dictionary<string, string>>();
            }

            return Ok(player);
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
                if (!_dal.Users.Any(u => u.Id == userId))
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
                    UserId = userId,
                    Assets = new Dictionary<string, Dictionary<string, string>>()
                };

                _dal.Players.Add(player);
                _dal.SaveChanges();

                return Ok(player);
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
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var userId = long.Parse(claim.Value);

                // On vérifie l'existence de l'utilisateur
                if (!_dal.Users.Any(u => u.Id == userId))
                    return Unauthorized();

                var player = _dal.Players.FirstOrDefault(p => p.Id == id);
                if (player == null) return NotFound();

                var campaign = _dal.Campaigns.FirstOrDefault(c => c.Id == player.CampaignId);
                if (campaign == null || campaign.Status != CampaignStatus.Preparation ||
                        (player.UserId != userId && campaign.CreatorId != userId))
                    return Unauthorized();

                _dal.Remove(player);
                _dal.SaveChanges();

                return NoContent();
            }
        }
    }
}
