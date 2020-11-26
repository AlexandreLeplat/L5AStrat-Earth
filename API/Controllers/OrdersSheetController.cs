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
using Entities.Interfaces;
using L5aStrat_Earth;
using Serilog;

namespace HostApp.Controllers
{
    // Classe ORDERSSHEET : gère les feuilles d'ordres
    [Route("api/orderssheets")]
    [ApiController]
    public class OrdersSheetController : ControllerBase
    {
        private readonly DAL _dal;
        private Dictionary<long, IGameEngine> _gameEngines;

        public OrdersSheetController(DAL dal)
        {
            _dal = dal;
            _gameEngines = new Dictionary<long, IGameEngine>();
        }

        private void InitializeGameEngine(long idGame)
        {
            if (!_gameEngines.ContainsKey(idGame))
            {
                _gameEngines.Add(idGame, new L5aStratEarthEngine(_dal)); // TODO : rendre l'initialisation du moteur de jeu dynamique
            }
        }

        // GET ordersSheets permet de récupérer la liste des feuilles d'ordres du joueur courant
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
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère les feuilles d'ordres du joueur courant
                    var ordersSheets = (from s in _dal.OrdersSheets
                                        where s.PlayerId == idPlayer
                                        orderby s.Turn descending
                                        select s).ToList();
                    if (ordersSheets == null) return NotFound();

                    return Ok(ordersSheets);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // GET ordersSheets/id permet de récupérer une feuille d'ordres spécifique du joueur courant
        [HttpGet("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult Get(long id)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère la feuille d'ordres ciblée
                    var ordersSheet = (from s in _dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == id
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    return Ok(ordersSheet);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        // GET ordersSheets/current permet de récupérer la feuille d'ordres actuelle du joueur courant
        [HttpGet("current")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCurrent()
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère la feuille d'ordres actuelle du joueur
                    var ordersSheet = (from s in _dal.OrdersSheets
                                       join p in _dal.Players on s.PlayerId equals p.Id
                                       join c in _dal.Campaigns on p.CampaignId equals c.Id
                                       where s.PlayerId == idPlayer && s.Turn == c.CurrentTurn
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();
                    
                    return Ok(ordersSheet);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // PUT ordersSheets/{id} permet de modifier la feuille d'ordres
        [HttpPut("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult Put(OrdersSheet input)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère la feuille d'ordres concernée
                    var ordersSheet = (from s in _dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == input.Id
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    if (ordersSheet.Status != OrdersSheetStatus.Writing)
                    {
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, "Statut incorrect de la feuille d'ordres");
                    }
                    ordersSheet.Priority = input.Priority;
                    _dal.SaveChanges();

                    return Ok(ordersSheet);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // POST ordersSheets/submit permet d'envoyer la feuille d'ordres actuelle du joueur courant
        [HttpPost("submit")]
        [EnableCors]
        [Authorize]
        public ActionResult PostSubmit()
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère la feuille d'ordres actuelle du joueur
                    var ordersSheet = (from s in _dal.OrdersSheets
                                       join p in _dal.Players on s.PlayerId equals p.Id
                                       join c in _dal.Campaigns on p.CampaignId equals c.Id
                                       where s.PlayerId == idPlayer && s.Turn == c.CurrentTurn
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    if (ordersSheet.SendDate.HasValue)
                    {
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, "La feuille d'ordre a déjà été envoyée");
                    } 
                    else if (ordersSheet.Status != OrdersSheetStatus.Writing)
                    {
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, "Statut incorrect de la feuille d'ordres");
                    }
                    ordersSheet.SendDate = DateTime.Now;
                    ordersSheet.Status = OrdersSheetStatus.Planned;
                    _dal.SaveChanges();

                    return Ok(ordersSheet);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // POST ordersSheets/check permet de contrôler la validité de la feuille d'ordres courante
        [HttpPost("check")]
        [EnableCors]
        [Authorize]
        public ActionResult PostCheckOrders()
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère la feuille d'ordres actuelle du joueur
                    var ordersSheet = (from s in _dal.OrdersSheets
                                       join p in _dal.Players on s.PlayerId equals p.Id
                                       join c in _dal.Campaigns on p.CampaignId equals c.Id
                                       where s.PlayerId == idPlayer && s.Turn == c.CurrentTurn
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    var gameId = (from p in _dal.Players
                                  join c in _dal.Campaigns on p.CampaignId equals c.Id
                                  where p.Id == ordersSheet.PlayerId
                                  select c.GameId).FirstOrDefault();
                    this.InitializeGameEngine(gameId);

                    var orders = this._gameEngines[gameId].CheckOrdersSheet(ordersSheet);
                    _dal.SaveChanges();

                    return Ok(orders.OrderBy(o => o.Rank));
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // GET ordersSheets/id/orders permet de récupérer les ordres de la feuille d'ordres ciblée du joueur courant
        [HttpGet("{id}/orders")]
        [EnableCors]
        [Authorize]
        public ActionResult GetOrdersSheetOrders(long id)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère les ordres de la feuille ciblée
                    var orders = (from o in _dal.Orders
                                  join s in _dal.OrdersSheets on o.OrdersSheetId equals s.Id
                                  where s.PlayerId == idPlayer && s.Id == id
                                  orderby o.Rank
                                  select o).ToList();
                    if (orders == null) return NotFound();

                    return Ok(orders);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // POST api/<OrderController>
        [HttpPost("{id}/orders")]
        [EnableCors]
        [Authorize]
        public ActionResult PostOrder(long id, [FromBody] OrderInputModel input)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère la feuille d'ordres ciblée
                    var ordersSheet = (from s in _dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == id
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    if (ordersSheet.Status != OrdersSheetStatus.Writing)
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, "Statut incorrect de la feuille d'ordres");

                    var ordersCount = (from o in _dal.Orders
                                       where o.OrdersSheetId == ordersSheet.Id
                                       select 1).Count();
                    if (ordersCount >= ordersSheet.MaxOrdersCount) return BadRequest("Nombre d'ordres max atteint");

                    var order = new Order()
                    {
                        ActionTypeId = input.ActionTypeId,
                        OrdersSheetId = ordersSheet.Id,
                        Parameters = input.Parameters,
                        Rank = input.Rank,
                        Status = OrderStatus.None
                    };

                    _dal.Orders.Add(order);
                    _dal.SaveChanges();

                    return Ok(order);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut("{idSheet}/orders/{idOrder}")]
        public ActionResult PutOrder(long idSheet, long idOrder, [FromBody] OrderInputModel input)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère la feuille d'ordres ciblée
                    var ordersSheet = (from s in _dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == idSheet
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    if (ordersSheet.Status != OrdersSheetStatus.Writing)
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, "Statut incorrect de la feuille d'ordres");

                    var order = (from o in _dal.Orders
                                 where o.OrdersSheetId == idSheet && o.Id == idOrder
                                 select o).FirstOrDefault();
                    if (order == null) return NotFound();

                    order.Parameters = input.Parameters;
                    order.Rank = input.Rank;
                    _dal.SaveChanges();

                    return Ok(order);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{idSheet}/orders/{idOrder}")]
        public ActionResult Delete(long idSheet, long idOrder)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère la feuille d'ordres ciblée
                    var ordersSheet = (from s in _dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == idSheet
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();
                    if (ordersSheet.Status != OrdersSheetStatus.Writing)
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, "Statut incorrect de la feuille d'ordres");

                    var order = (from o in _dal.Orders
                                 where o.OrdersSheetId == idSheet && o.Id == idOrder
                                 select o).FirstOrDefault();
                    if (order == null) return NotFound();

                    _dal.Orders.Remove(order);
                    _dal.SaveChanges();

                    return StatusCode((int)HttpStatusCode.NoContent);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
