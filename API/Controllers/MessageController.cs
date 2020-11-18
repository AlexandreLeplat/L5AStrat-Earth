using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Entities.Database;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace HostApp.Controllers
{
    // Classe MESSAGE : gère les messages et notifications
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly DAL _dal;

        public MessageController(DAL dal)
        {
            _dal = dal;
        }

        // GET messages/id permet de récupérer un message
        [HttpGet("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult GetMessage(long? id)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    var message = (from m in _dal.Messages
                                    where m.Id == id.Value && (m.PlayerId == idPlayer || m.SenderId == idPlayer)
                                    select m).FirstOrDefault();
                    if (message == null) return NotFound();

                    return Ok(message);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // GET messages permet de récupérer la liste des messages du joueur courant
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult GetMessages(int pageSize = 10, int pageIndex = 0)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère les messages du joueur courant
                    var skip = pageIndex * pageSize;
                    var messages = (from m in _dal.Messages
                                        where m.PlayerId == idPlayer && !m.IsDeleted
                                        orderby m.SendDate descending
                                        select m).Skip(skip).Take(pageSize).ToList();
                    if (messages == null) return NotFound();

                    return Ok(messages);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // GET messages/sent permet de récupérer la liste des messages envoyés par le joueur courant
        [HttpGet("sent")]
        [EnableCors]
        [Authorize]
        public ActionResult GetSentMessages(int pageSize = 10, int pageIndex = 0)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère les messages du joueur courant
                    var skip = pageIndex * pageSize;
                    var messages = (from m in _dal.Messages
                                    where m.SenderId == idPlayer && !m.IsDeletedForSender
                                    orderby m.SendDate descending
                                    select m).Skip(skip).Take(pageSize).ToList();
                    if (messages == null) return NotFound();

                    return Ok(messages);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // GET messages/count permet de récupérer le nombre de messages non lus du joueur
        [HttpGet("count")]
        [EnableCors]
        [Authorize]
        public ActionResult GetCount(string category = null)
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    var messageCount = (from m in _dal.Messages
                                where (m.PlayerId == idPlayer && !m.IsDeleted // total des messages
                                        || category == "sent" && m.SenderId == idPlayer && !m.IsDeletedForSender) // messages envoyés
                                        && (category != "unread" || !m.IsRead) // messages non lus
                                select m).Count();

                    return Ok(messageCount);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // POST messages permet d'envoyer un message
        [HttpPost]
        [EnableCors]
        [Authorize]
        public ActionResult PostMessage(Message message)
        {
            if (message == null)
            {
                return BadRequest("Paramètre manquant");
            }
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On vérifie que le mesage contient bien un sujet et un corps de texte
                    if (string.IsNullOrWhiteSpace(message.Subject) 
                        || message.Subject.Length > 100
                        || string.IsNullOrWhiteSpace(message.Body)
                        || message.Body.Length > 10000)
                        return BadRequest();

                    // On vérifie que le destinaire est bien sur la même campagne que le joueur
                    var recipientExists = (from p in _dal.Players
                                           join r in _dal.Players on p.CampaignId equals r.CampaignId
                                           where p.Id == idPlayer && r.Id == message.PlayerId
                                           select r).Any();
                    if (!recipientExists) return NotFound();

                    message.SenderId = idPlayer;
                    message.SendDate = DateTime.Now;
                    message.IsNotification = false;
                    message.IsRead = false;
                    message.IsArchived = false;

                    _dal.Messages.Add(message);
                    _dal.SaveChanges();

                    return Ok(message);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // PUT messages permet de mettre à jour les champs isRead et isArchived d'un message
        [HttpPut("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult PutMessage(long? id, Message message)
        {
            if (!id.HasValue || message == null)
            {
                return BadRequest("Paramètre manquant");
            }
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère le message à mettre à jour
                    var dbMessage = (from m in _dal.Messages
                                     where m.PlayerId == idPlayer && m.Id == id && !m.IsDeleted
                                     select m).FirstOrDefault();
                    if (dbMessage == null) return NotFound();

                    dbMessage.IsRead = message.IsRead;
                    dbMessage.IsArchived = message.IsArchived;
                    _dal.SaveChanges();

                    return Ok(message);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // DELETE messages permet de supprimer un message
        [HttpDelete("{id}")]
        [EnableCors]
        [Authorize]
        public ActionResult DeleteMessage(long? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Paramètre manquant");
            }
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();
                    var idPlayer = long.Parse(claim.Value);

                    // On récupère le message à mettre à jour
                    var dbMessage = (from m in _dal.Messages
                                     where (m.PlayerId == idPlayer && !m.IsDeleted 
                                            || m.SenderId == idPlayer && !m.IsDeletedForSender)
                                            && m.Id == id
                                     select m).FirstOrDefault();
                    if (dbMessage == null) return NotFound();

                    if (dbMessage.PlayerId == idPlayer) dbMessage.IsDeleted = true;
                    if (dbMessage.SenderId == idPlayer) dbMessage.IsDeletedForSender = true;
                    _dal.SaveChanges();

                    return NoContent();
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
