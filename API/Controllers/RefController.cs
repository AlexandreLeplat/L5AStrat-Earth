using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Entities.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HostApp.Controllers
{
    // Classe Référentiel : publie les référentiels utiles
    [Route("api/ref")]
    [ApiController]
    public class RefController : ControllerBase
    {
        private readonly DAL _dal;

        public RefController(DAL dal)
        {
            _dal = dal;
        }

        // GET ActionTypes : fournit les types d'actions jouables dans une feuille d'ordre
        [HttpGet("actiontypes")]
        [EnableCors]
        [Authorize]
        public ActionResult GetActionTypes()
        {
            using (_dal)
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();

                // On récupère le système de jeu de la campagne associée au compte joueur courant
                var id = long.Parse(claim.Value);
                var actionTypes = (from a in _dal.ActionTypes
                            join c in _dal.Campaigns on a.GameId equals c.GameId
                            join p in _dal.Players on c.Id equals p.CampaignId
                            where p.Id == id
                            orderby a.Label
                            select a).ToList();
                if (actionTypes == null) return NotFound();

                return Ok(actionTypes);
            }
        }
    }
}
