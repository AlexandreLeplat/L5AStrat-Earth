using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Entities.Database;
using Entities.Enums;
using Entities.Models;
using HostApp.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HostApp.Controllers
{
    // Classe CAMPAIGNS : gère les informations liées aux campagnes
    [Route("api/campaigns")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly DAL _dal;

        public CampaignController(DAL dal)
        {
            _dal = dal;
        }
        
        // GET campaigns permet de récupérer la liste des campagnes
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult GetCampaigns(int? status = null)
        {
            var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
            if (claim == null) return Unauthorized();
            var userId = long.Parse(claim.Value);

            using (_dal)
            {
                // On vérifie l'existence de l'utilisateur
                if (!_dal.Users.Any(u => u.Id == userId))
                    return Unauthorized();

                // On récupère la liste des campagnes
                var campaigns = (from c in _dal.Campaigns
                                where status == null || (int)c.Status == status
                                select c).ToList();

                return Ok(campaigns);
            }
        }

        // GET campaign permet de récupérer une campagne
        [HttpGet("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCampaign(long? id)
        {
            var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
            if (claim == null) return Unauthorized();
            var userId = long.Parse(claim.Value);

            using (_dal)
            {
                // On vérifie l'existence de l'utilisateur
                if (!_dal.Users.Any(u => u.Id == userId))
                    return Unauthorized();

                // On récupère la campagne
                var campaign = (from c in _dal.Campaigns
                                 where c.Id == id.Value
                                 select c).FirstOrDefault();
                if (campaign == null) return NotFound();

                return Ok(campaign);
            }
        }

        // GET campaign permet de récupérer une campagne
        [HttpPost]
        [EnableCors]
        [Authorize]
        public ActionResult PostCampaign(Campaign model)
        {
            var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
            if (claim == null) return Unauthorized();
            var userId = long.Parse(claim.Value);

            using (_dal)
            {
                // On vérifie l'existence de l'utilisateur
                if (!_dal.Users.Any(u => u.Id == userId))
                    return Unauthorized();

                if (model == null)
                    return BadRequest("Paramètres manquants");

                if (string.IsNullOrWhiteSpace(model.Name) || model.Name.Length > 50)
                    return BadRequest("Nom de campagne incorrect");

                if (_dal.Campaigns.Any(c => c.Name == model.Name.Trim()))
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, "Une campagne avec ce nom existe déjà");

                var gameId = _dal.Games.First().Id; // TODO : choix du système de jeu

                if (model.PhaseLength == 0) model.PhaseLength = 1440;
                if (model.NextPhase < DateTime.Now)
                {
                    var hours = model.NextPhase.Hour;
                    model.NextPhase = DateTime.Now.Date.AddHours(hours);
                }

                var campaign = new Campaign()
                {
                    Name = model.Name.Trim(),
                    PhaseLength = model.PhaseLength,
                    NextPhase = model.NextPhase,
                    GameId = gameId,
                    Status = CampaignStatus.Preparation,
                    Assets = new Dictionary<string, Dictionary<string, string>>(),
                    CreatorId = userId
                };

                _dal.Campaigns.Add(campaign);
                _dal.SaveChanges();

                var adminPlayer = new Player()
                {
                    Name = "Neutre",
                    Color = "lightgrey",
                    CampaignId = campaign.Id,
                    UserId = 1, // TODO : récupérer l'idUser admin dans la config
                    IsAdmin = true
                };
                _dal.Players.Add(adminPlayer);
                _dal.SaveChanges();

                return Ok(campaign);
            }
        }

        // DELETE campaign permet de supprimer une campagne en préparation
        [HttpDelete("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult DeleteCampaign(long? id)
        {
            var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
            if (claim == null) return Unauthorized();
            var userId = long.Parse(claim.Value);

            using (_dal)
            {
                // On vérifie l'existence de l'utilisateur
                if (!_dal.Users.Any(u => u.Id == userId))
                    return Unauthorized();

                // On récupère la campagne ciblée pour vérifier les prérequis
                var campaign = _dal.Campaigns.FirstOrDefault(c => c.Id == id.Value);
                if (campaign == null) return NotFound();

                if (campaign.CreatorId != userId)
                    return Unauthorized("Vous n'êtes pas autorisé à effecuter cette action");

                if (campaign.Status != CampaignStatus.Preparation)
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, "Le statut de la campagne ne permet pas cette action");

                var campaignPlayers = _dal.Players.Where(p => p.CampaignId == campaign.Id).ToList();
                _dal.Players.RemoveRange(campaignPlayers);

                _dal.SaveChanges();

                return Ok(campaign);
            }
        }

        // GET campaigns/current permet de récupérer la campagne courante
        [HttpGet("current")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCurrent()
        {
            var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
            if (claim == null) return Unauthorized();

            using (_dal)
            {
                // On récupère la campagne associée au compte joueur courant
                var idPlayer = long.Parse(claim.Value);
                var campaign = (from c in _dal.Campaigns
                                join p in _dal.Players on c.Id equals p.CampaignId
                             where p.Id == idPlayer
                             select c).FirstOrDefault();
                if (campaign == null) return NotFound();

                return Ok(campaign);
            }
        }

        // POST campaigns/id/work permet de traiter les ordres et faire avancer les tours de la campagne désignée
        [HttpPost("{id}/work")]
        [EnableCors]
        [Authorize]
        public ActionResult Work(long? id)
        {
            var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
            if (claim == null) return Unauthorized();

            using (_dal)
            {
                Campaign campaign;
                using (var transaction = _dal.Database.BeginTransaction())
                {
                    // On récupère la campagne ciblée
                    campaign = (from c in _dal.Campaigns
                                    where c.Id == id
                                    select c).FirstOrDefault();
                    if (campaign == null) return NotFound();

                    // Si la campagne est déjà en cours de traitement, en erreur ou terminée, on ne fait rien
                    if (campaign.Status != CampaignStatus.Running)
                    {
                        return Ok(campaign);
                    }

                    campaign.Status = CampaignStatus.Treating;
                    _dal.SaveChanges();
                    transaction.Commit();
                }

                var orderEngine = new OrdersEngine(_dal);
                orderEngine.Work(campaign);

                if (campaign.Status == CampaignStatus.Treating)
                    campaign.Status = CampaignStatus.Running;

                _dal.SaveChanges();

                return Ok(campaign);
            }
        }

        // POST campaigns/id/start permet de démarrer une partie
        [HttpPost("{id}/start")]
        [EnableCors]
        [Authorize]
        public ActionResult Start(long? id)
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var userId = long.Parse(claim.Value);

                var user = (from u in _dal.Users
                            where u.Id == userId
                            select u).FirstOrDefault();
                if (user == null) return Unauthorized();

                var campaign = (from c in _dal.Campaigns
                                where c.Id == id.Value
                                select c).FirstOrDefault();
                if (campaign == null) return NotFound();

                var playerUsers = (from p in _dal.Players
                                   where p.CampaignId == id.Value
                                   select p).ToList();

                if (user.Role != UserRole.Admin && campaign.CreatorId != userId && !playerUsers.Any(p => p.UserId == userId))
                    return Unauthorized();

                var lobbyEngine = new LobbyEngine(_dal);
                lobbyEngine.LaunchGame(campaign);
                playerUsers.ForEach(p => p.Status = PlayerStatus.Ready);
                _dal.SaveChanges();

                return Ok();
            }
        }

        // POST campaigns/id/reset permet de redémarrer une partie à zéro
        [HttpPost("{id}/skip")]
        [EnableCors]
        [Authorize]
        public ActionResult SkipPhase(long? id)
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var idPlayer = long.Parse(claim.Value);

                var user = (from u in _dal.Users
                            join p in _dal.Players on u.Id equals p.UserId
                            join p2 in _dal.Players on u.Id equals p2.UserId
                            join c in _dal.Campaigns on p2.CampaignId equals c.Id
                            where p.Id == idPlayer && c.Id == id.Value
                            select u).FirstOrDefault();
                if (user == null || user.Role != UserRole.Admin) return Unauthorized();

                var campaign = (from c in _dal.Campaigns
                                where c.Id == id.Value
                                select c).FirstOrDefault();
                if (campaign == null) return NotFound();

                campaign.NextPhase = campaign.NextPhase.AddMinutes(-(campaign.PhaseLength));
                _dal.SaveChanges();

                return Ok();
            }
        }

        // POST campaigns/id/reset permet de redémarrer une partie à zéro
        [HttpPost("{id}/reset")]
        [EnableCors]
        [Authorize]
        public ActionResult ResetCampaign(long? id)
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var idPlayer = long.Parse(claim.Value);

                var user = (from u in _dal.Users
                            join p in _dal.Players on u.Id equals p.UserId
                            join p2 in _dal.Players on u.Id equals p2.UserId
                            join c in _dal.Campaigns on p2.CampaignId equals c.Id
                            where p.Id == idPlayer && c.Id == id.Value
                            select u).FirstOrDefault();
                if (user == null || user.Role != UserRole.Admin) return Unauthorized();

                var campaign = (from c in _dal.Campaigns
                                where c.Id == id.Value
                                select c).FirstOrDefault();
                if (campaign == null) return NotFound();

                campaign.CurrentTurn = 0;
                campaign.CurrentPhase = TurnPhase.End;
                campaign.NextPhase = DateTime.Now;
                campaign.Status = CampaignStatus.Preparation;

                _dal.Units.RemoveRange((from u in _dal.Units
                                        join p in _dal.Players on u.PlayerId equals p.Id
                                        where p.CampaignId == campaign.Id
                                        select u).ToArray());

                _dal.Orders.RemoveRange((from o in _dal.Orders
                                           join s in _dal.OrdersSheets on o.OrdersSheetId equals s.Id
                                           join p in _dal.Players on s.PlayerId equals p.Id
                                           where p.CampaignId == campaign.Id
                                           select o).ToArray());

                _dal.OrdersSheets.RemoveRange((from s in _dal.OrdersSheets
                                               join p in _dal.Players on s.PlayerId equals p.Id
                                               where p.CampaignId == campaign.Id
                                               select s).ToArray());

                _dal.MapTiles.RemoveRange((from t in _dal.MapTiles
                                           join m in _dal.Maps on t.MapId equals m.Id
                                           join p in _dal.Players on m.PlayerId equals p.Id
                                           where p.CampaignId == campaign.Id
                                           select t).ToArray());

                _dal.Maps.RemoveRange((from m in _dal.Maps
                                       join p in _dal.Players on m.PlayerId equals p.Id
                                       where p.CampaignId == campaign.Id
                                       select m).ToArray());

                _dal.Messages.RemoveRange((from m in _dal.Messages
                                           join p in _dal.Players on m.PlayerId equals p.Id
                                           where p.CampaignId == campaign.Id && m.IsNotification
                                           select m).ToArray());

                _dal.SaveChanges();

                var lobbyEngine = new LobbyEngine(_dal);
                lobbyEngine.LaunchGame(campaign);
                _dal.SaveChanges();

                return Ok();
            }
        }

        // GET campaigns/current/players permet de récupérer la liste des joueurs de la campagne courante
        [HttpGet("current/players")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCurrentCampaignPlayers()
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var idPlayer = long.Parse(claim.Value);

                // On récupère les joueurs ayant le même idCampaign que le joueur courant
                var players = (from c in _dal.Players
                               join p in _dal.Players on c.CampaignId equals p.CampaignId
                               where c.Id == idPlayer
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

        // GET campaigns/id/players permet de récupérer la liste des joueurs de la campagne ciblée
        [HttpGet("{id}/players")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCampaignPlayers(long? id)
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var userId = long.Parse(claim.Value);

                // On vérifie l'existence de l'utilisateur
                if (!_dal.Users.Any(u => u.Id == userId))
                    return Unauthorized();

                // On récupère les joueurs ayant le même idCampaign que le joueur courant
                var playersList = (from p in _dal.Players
                               join u in _dal.Users on p.UserId equals u.Id
                               where p.CampaignId == id.Value && !p.IsAdmin
                               select new Tuple<Player, string>(p, u.Name)).ToList();

                // On filtre les informations pour n'afficher que ce que le joueur a le droit de voir
                var players = playersList.Select(t => new Player()
                {
                    Id = t.Item1.Id,
                    Name = t.Item1.Name,
                    Color = t.Item1.Color,
                    UserId = t.Item1.UserId,
                    CampaignId = t.Item1.CampaignId,
                    UserName = t.Item2,
                    Status = t.Item1.Status
                }).ToList();

                return Ok(players);
            }
        }

        // GET campaigns/{id}/randomplayer permet de proposer un joueur aléatoire pour s'inscrire à la partie
        [HttpGet("{id}/randomplayer")]
        [EnableCors]
        [Authorize]
        public ActionResult GetRandomPlayerForCampaign(long? id)
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var userId = long.Parse(claim.Value);

                // On vérifie l'existence de l'utilisateur
                if (!_dal.Users.Any(u => u.Id == userId))
                    return Unauthorized();

                // On récupère la campagne ciblée pour vérifier les prérequis
                var campaign = _dal.Campaigns.FirstOrDefault(c => c.Id == id.Value);
                if (campaign == null) return NotFound();

                if (campaign.Status != CampaignStatus.Preparation)
                    return StatusCode((int)HttpStatusCode.PreconditionFailed, "Le statut de la campagne ne permet pas cette action");
                var campaignPlayers = _dal.Players.Where(p => p.CampaignId == campaign.Id && !p.IsAdmin).ToList();

                var lobbyEngine = new LobbyEngine(_dal);
                var randomPlayer = lobbyEngine.GenerateRandomPlayer(campaign, userId);

                return Ok(randomPlayer);
            }
        }
    }
}
