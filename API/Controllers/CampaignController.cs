﻿using System;
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
        public ActionResult Get()
        {
            return NotFound();
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

                    // Si la campagne est déjà en cours de traitement, en erreur ou terminée, on rend une erreur
                    if (campaign.Status != CampaignStatus.Running)
                    {
                        return StatusCode((int)HttpStatusCode.Conflict);
                    }

                    campaign.Status = CampaignStatus.Treating;
                    _dal.SaveChanges();
                    transaction.Commit();
                }

                var orderEngine = new OrdersEngine(_dal);
                orderEngine.Work(campaign);

                campaign.Status = CampaignStatus.Running;
                _dal.SaveChanges();

                return Ok(campaign);
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
                                           where p.CampaignId == campaign.Id
                                           select m).ToArray());

                _dal.SaveChanges();

                var orderEngine = new OrdersEngine(_dal);
                orderEngine.Work(campaign);
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

        // GET campaigns/migrate applique les changements de dernière version
        [HttpGet("migrate")]
        [EnableCors]
        [Authorize]
        public ActionResult Migrate()
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();
                var idPlayer = long.Parse(claim.Value);

                var user = (from u in _dal.Users
                            join p in _dal.Players on u.Id equals p.UserId
                            where p.Id == idPlayer
                            select u).FirstOrDefault();
                if (user == null || user.Role != UserRole.Admin) return Unauthorized();

                var adminPlayers = (from p in _dal.Players
                                    where p.IsAdmin
                                    select p).ToList();
                foreach(var admin in adminPlayers)
                {
                    admin.Name = "Neutre";
                    admin.Color = "lightgrey";
                }

                var campaigns = (from c in _dal.Campaigns
                                select c).ToList();
                if (campaigns == null) return NotFound();
                foreach(var campaign in campaigns) 
                {
                    var units = (from u in _dal.Units
                                 join p in _dal.Players on u.PlayerId equals p.Id
                                 join c in _dal.Campaigns on p.CampaignId equals c.Id
                                 where c.Id == campaign.Id
                                 select u).ToList();
                    foreach(var building in units.Where(u => u.Type == "Building"))
                    {
                        if (adminPlayers.Select(a => a.Id).Contains(building.PlayerId))
                        {
                            foreach(var army in units.Where(u => u.Type == "Army"))
                            {
                                if (building.X == army.X && building.Y == army.Y)
                                {
                                    building.PlayerId = army.PlayerId;
                                }
                            }
                        }
                    }
                    foreach(var army in units.Where(u => u.Type == "Army"))
                    {
                        var armyAssets = new Dictionary<string, Dictionary<string, string>>();
                        foreach (var a in army.Assets) { armyAssets.Add(a.Key, a.Value); }
                        armyAssets.Add("Renown", new Dictionary<string, string>() { { "3", null } });
                        army.Assets = armyAssets;
                    }
                }
                _dal.SaveChanges();

                return Ok();
            }
        }
    }
}
