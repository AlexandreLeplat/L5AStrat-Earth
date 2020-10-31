using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Entities.Database;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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

        // GET messages permet de récupérer la liste des messages du joueur courant
        [HttpGet]
        [EnableCors]
        [Authorize]
        public ActionResult GetMessages()
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    // On récupère les messages du joueur courant
                    var id = long.Parse(claim.Value);
                    var messages = (from m in _dal.Messages
                                        where m.PlayerId == id || m.PlayerId == 0
                                        orderby m.SendDate descending
                                        select m).ToList();
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
        public ActionResult GetCount()
        {
            try
            {
                using (_dal)
                {
                    var claim = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
                    if (claim == null) return Unauthorized();

                    var id = long.Parse(claim.Value);
                    var messageCount = (from m in _dal.Messages
                                where m.PlayerId == id && !m.IsRead
                                select m).Count();

                    return Ok(messageCount);
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

                    // On récupère le message à mettre à jour
                    var idPlayer = long.Parse(claim.Value);
                    var dbMessage = (from m in _dal.Messages
                                    where m.PlayerId == idPlayer && m.Id == id
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

                    // On récupère le message à mettre à jour
                    var idPlayer = long.Parse(claim.Value);
                    var dbMessage = (from m in _dal.Messages
                                     where m.PlayerId == idPlayer && m.Id == id
                                     select m).FirstOrDefault();
                    if (dbMessage == null) return NotFound();

                    _dal.Messages.Remove(dbMessage);
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
