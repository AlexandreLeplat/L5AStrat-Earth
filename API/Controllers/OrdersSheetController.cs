using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Database;
using API.Models;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // Classe ORDERSSHEET : gère les feuilles d'ordres
    [Route("orderssheets")]
    [ApiController]
    public class OrdersSheetController : ControllerBase
    {
        // GET ordersSheets permet de récupérer la liste des feuilles d'ordres du joueur courant
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult Get()
        {
            try
            {
                using (var dal = new DAL())
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère les feuilles d'ordres du joueur courant
                    var id = long.Parse(claim.Value);
                    var ordersSheets = (from s in dal.OrdersSheets
                                        where s.PlayerId == id
                                        select s).ToList();
                    if (ordersSheets == null) return NotFound();

                    return Ok(ordersSheets);
                }
            }
            catch (Exception e)
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
                using (var dal = new DAL())
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère la feuille d'ordres ciblée
                    var idPlayer = long.Parse(claim.Value);
                    var ordersSheet = (from s in dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == id
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    return Ok(ordersSheet);
                }
            }
            catch (Exception e)
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
                using (var dal = new DAL())
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère la feuille d'ordres actuelle du joueur
                    var idPlayer = long.Parse(claim.Value);
                    var ordersSheet = (from s in dal.OrdersSheets
                                       join p in dal.Players on s.PlayerId equals p.Id
                                       join c in dal.Campaigns on p.CampaignId equals c.Id
                                       where s.PlayerId == idPlayer && s.Turn == c.CurrentTurn
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    return Ok(ordersSheet);
                }
            }
            catch (Exception e)
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
                using (var dal = new DAL())
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère la feuille d'ordres concernée
                    var idPlayer = long.Parse(claim.Value);
                    var ordersSheet = (from s in dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == input.Id
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    if (ordersSheet.Status != OrdersSheetStatus.Writing)
                    {
                        return StatusCode((int)HttpStatusCode.PreconditionFailed, "Statut incorrect de la feuille d'ordres");
                    }
                    ordersSheet.Priority = input.Priority;
                    dal.SaveChanges();

                    return Ok(ordersSheet);
                }
            }
            catch (Exception e)
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
                using (var dal = new DAL())
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère la feuille d'ordres actuelle du joueur
                    var idPlayer = long.Parse(claim.Value);
                    var ordersSheet = (from s in dal.OrdersSheets
                                       join p in dal.Players on s.PlayerId equals p.Id
                                       join c in dal.Campaigns on p.CampaignId equals c.Id
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
                    dal.SaveChanges();

                    return Ok(ordersSheet);
                }
            }
            catch (Exception e)
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
                using (var dal = new DAL())
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère les ordres de la feuille ciblée
                    var idPlayer = long.Parse(claim.Value);
                    var orders = (from o in dal.Orders
                                  join s in dal.OrdersSheets on o.OrdersSheetId equals s.Id
                                  where s.PlayerId == idPlayer && s.Id == id
                                  orderby o.Rank
                                  select o).ToList();
                    if (orders == null) return NotFound();

                    return Ok(orders);
                }
            }
            catch (Exception e)
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
                using (var dal = new DAL())
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère la feuille d'ordres ciblée
                    var idPlayer = long.Parse(claim.Value);
                    var ordersSheet = (from s in dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == id
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    var ordersCount = (from o in dal.Orders
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

                    dal.Orders.Add(order);
                    dal.SaveChanges();

                    return Ok(order);
                }
            }
            catch (Exception e)
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
                using (var dal = new DAL())
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère la feuille d'ordres ciblée
                    var idPlayer = long.Parse(claim.Value);
                    var ordersSheet = (from s in dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == idSheet
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    var order = (from o in dal.Orders
                                 where o.OrdersSheetId == idSheet && o.Id == idOrder
                                 select o).FirstOrDefault();
                    if (order == null) return NotFound();

                    order.Parameters = input.Parameters;
                    order.Rank = input.Rank;
                    dal.SaveChanges();

                    return Ok(order);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/<OrderController>/5
        [HttpPut("{idSheet}/orders")]
        public ActionResult PutOrders(long idSheet, [FromBody] OrderInputModel[] input)
        {
            try
            {
                if (input == null)
                    return BadRequest("PUT Orders : body manquant");

                using (var dal = new DAL())
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère la feuille d'ordres ciblée
                    var idPlayer = long.Parse(claim.Value);
                    var ordersSheet = (from s in dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == idSheet
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    var orders = (from o in dal.Orders
                                    where o.OrdersSheetId == idSheet
                                    select o).ToList();
                    if (orders == null) return NotFound();

                    foreach (var item in input)
                    {
                        var order = orders.FirstOrDefault(o => o.Id == item.Id);
                        if (order == null)
                        {
                            return NotFound(string.Format("Ordre non trouvé : {0}", item.Id));
                        }
                        order.Parameters = item.Parameters;
                        order.Rank = item.Rank;
                    }
                    dal.SaveChanges();

                    return Ok(orders);
                }
            }
            catch (Exception e)
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
                using (var dal = new DAL())
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère la feuille d'ordres ciblée
                    var idPlayer = long.Parse(claim.Value);
                    var ordersSheet = (from s in dal.OrdersSheets
                                       where s.PlayerId == idPlayer && s.Id == idSheet
                                       select s).FirstOrDefault();
                    if (ordersSheet == null) return NotFound();

                    var order = (from o in dal.Orders
                                 where o.OrdersSheetId == idSheet && o.Id == idOrder
                                 select o).FirstOrDefault();
                    if (order == null) return NotFound();

                    dal.Orders.Remove(order);
                    dal.SaveChanges();

                    return StatusCode((int)HttpStatusCode.NoContent);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
