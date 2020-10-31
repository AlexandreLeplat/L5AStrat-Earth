using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using HostApp.Models;
using Entities.Database;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace HostApp.Controllers
{
    // Classe MAP : gère les cartes
    [Route("api/maps")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly DAL _dal;

        public MapController(DAL dal)
        {
            _dal = dal;
        }

        // GET maps permet de récupérer la liste des cartes du joueur courant
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult Get()
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var id = long.Parse(claim.Value);

                    var adminId = (from p1 in _dal.Players
                                   join p2 in _dal.Players on p1.CampaignId equals p2.CampaignId
                                   where p1.Id == id && p2.IsAdmin
                                   select p2.Id).FirstOrDefault();

                    // On récupère les cartes du joueur courant ou du joueur admin de la partie
                    var maps = (from m in _dal.Maps
                                where m.PlayerId == id || m.PlayerId == adminId
                                orderby m.CreationDate descending
                                select m).ToList();
                    if (maps == null) return NotFound();

                    return Ok(maps);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // GET maps/{id}/tiles permet de récupérer les cases de la carte sélectionnée
        [HttpGet("{id}/tiles")]
        [EnableCors]
        [Authorize]
        public ActionResult GetTiles(long? id)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère la carte la plus récente du joueur courant
                    var tiles = (from t in _dal.MapTiles
                               where t.MapId == id
                               orderby t.Y, t.X 
                               select t).ToList();
                    if (tiles == null) return NotFound();

                    var result = new Dictionary<int, List<MapTile>>();
                    foreach(var tile in tiles)
                    {
                        if (!result.ContainsKey(tile.Y))
                        {
                            result.Add(tile.Y, new List<MapTile>());
                        }
                        result[tile.Y].Add(tile);
                    }

                    return Ok(result.Values.ToList());
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
