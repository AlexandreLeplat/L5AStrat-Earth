using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using API.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Classe Référentiel : publie les référentiels utiles
    [Route("ref")]
    [ApiController]
    public class RefController : ControllerBase
    {
        // GET ActionTypes : fournit les types d'actions jouables dans une feuille d'ordre
        [HttpGet("actiontypes")]
        [EnableCors]
        [Authorize]
        public ActionResult GetActionTypes()
        {
            using (var dal = new DAL())
            {
                var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                if (claim == null) return Unauthorized();

                // On récupère le système de jeu de la campagne associée au compte joueur courant
                var id = long.Parse(claim.Value);
                var actionTypes = (from a in dal.ActionTypes
                            join c in dal.Campaigns on a.GameId equals c.GameId
                            join p in dal.Players on c.Id equals p.CampaignId
                            where p.Id == id
                            select a).ToList();
                if (actionTypes == null) return NotFound();

                return Ok(actionTypes);
            }
        }
    }
}
