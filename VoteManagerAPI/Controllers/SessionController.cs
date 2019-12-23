using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VoteManagerAPI.Services;

namespace VoteManagerAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Session")]
    public class SessionController : ApiController
    {
        [HttpPost, Route("Begin")]
        [Authorize(Roles = "Admin, Chair")]
        public async Task<IHttpActionResult> InitializeSession()
        {
            var service = GetSessionService();
            if (await service.StartNewSession())
                return Ok("Started new session.");

            return BadRequest("Session already in progress.");
        }

        [HttpGet, Route("Current")]
        public async Task<IHttpActionResult> GetCurrentSession()
        {
            var service = GetSessionService();
            var currentSessionId = await service.GetCurrentSessionId();
            var currentSession = await service.GetSessionById(currentSessionId);
            return Ok(currentSession);
        }

        [HttpGet, Route("{sessionId}")]
        public async Task<IHttpActionResult> GetSessionById(int sessionId)
        {
            var service = GetSessionService();
            var session = await service.GetSessionById(sessionId);
            return Ok(session);
        }

        [HttpPut, Route("End")]
        [Authorize(Roles = "Admin, Chair")]
        public async Task<IHttpActionResult> EndCurrentSession()
        {
            var service = GetSessionService();
            if (await service.EndCurrentSession())
                return Ok("Session ended successfully.");

            return BadRequest("Cannot end session.");
        }

        private SessionService GetSessionService() => new SessionService(User.Identity.GetUserId());
    }
}
