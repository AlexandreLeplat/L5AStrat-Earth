using API.Models;
using API.Security;
using Entities;
using Entities.Database;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/<TokenController>
        [HttpGet]
        [Authorize]
        public ActionResult Get()
        {
            var jwt = new JWTHelper(_config);
            var name = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            var token = jwt.GenerateSecurityToken(new User() { Id = 0, Name = name, Role = UserRole.None });
            return Ok(token);
        }

        // POST api/<TokenController>
        [HttpPost]
        public ActionResult Post([FromBody] TokenInputModel input)
        {
            var jwt = new JWTHelper(_config);
            // Mot de passe = "Togashi"

            using (var dal = new DAL())
            {
                var user = dal.Users.FirstOrDefault(u => u.Name == input.Name);
                if (user == null)
                {
                    return Unauthorized();
                }
                if (BCrypt.Net.BCrypt.Verify(input.Password, user.Password))
                {
                    var token = jwt.GenerateSecurityToken(new User() { Id = 0, Name = input.Name, Role = UserRole.None });
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }
    }
}
