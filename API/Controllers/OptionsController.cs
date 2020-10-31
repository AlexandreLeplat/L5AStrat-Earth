using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Entities.Database;
using Entities.Enums;
using Entities.Interfaces;
using Entities.Models;
using HostApp.Business;
using L5aStrat_Earth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HostApp.Controllers
{
    // Classe OPTIONS : gère les listes d'options
    [Route("api/options")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly DAL _dal;
        private Dictionary<long, IGameEngine> _gameEngines;

        public OptionsController(DAL dal)
        {
            _dal = dal;
            _gameEngines = new Dictionary<long, IGameEngine>();
        }
        
        // GET options permet de récupérer une liste d'items achetables
        [HttpGet("{resource}")]
        [EnableCors]
        [Authorize]
        public ActionResult Get(string resource, [FromQuery]string parameters)
        {
            var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
            if (claim == null) return Unauthorized();

            if (string.IsNullOrEmpty(parameters)) parameters = string.Empty;
            using (_dal)
            {
                // On récupère la campagne associée au compte joueur courant
                var id = long.Parse(claim.Value);
                var campaign = (from c in _dal.Campaigns
                                join p in _dal.Players on c.Id equals p.CampaignId
                                where p.Id == id
                                select c).FirstOrDefault();
                if (campaign == null) return NotFound();

                this.InitializeGameEngine(campaign.GameId);

                var result = _gameEngines[campaign.GameId].GetOptionsList(resource, id, parameters.Split(';'));

                return Ok(result);
            }
        }

        private void InitializeGameEngine(long idGame)
        {
            if (!_gameEngines.ContainsKey(idGame))
            {
                _gameEngines.Add(idGame, new L5aStratEarthEngine(_dal)); // TODO : rendre l'initialisation du moteur de jeu dynamique
            }
        }

    }
}
