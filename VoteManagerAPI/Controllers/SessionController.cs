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
        // CREATE
        [HttpPost, Route("Begin")]
        [Authorize(Roles = "Admin, Chair")]
        public async Task<IHttpActionResult> InitializeSession()
        {
            var service = GetSessionService();
            if (await service.StartNewSession())
                return Ok("Started new session.");

            return BadRequest("Session already in progress.");
        }

        // GET Current
        [HttpGet, Route("Current")]
        public async Task<IHttpActionResult> GetCurrentSession()
        {
            var service = GetSessionService();
            var currentSessionId = await service.GetCurrentSessionId();
            if (currentSessionId != 0)
            {
                var currentSession = await service.GetSessionById(currentSessionId);
                return Ok(currentSession);
            }

            return BadRequest("There is no active session.");
        }

        // GET All
        [HttpGet, Route("All")]
        public async Task<IHttpActionResult> GetAllSessions()
        {
            var service = GetSessionService();
            var sessions = await service.GetAllSessions();
            return Ok(sessions);
        }

        // GET by ID
        [HttpGet, Route("{sessionId:int}")]
        public async Task<IHttpActionResult> GetSessionById(int sessionId)
        {
            var service = GetSessionService();
            var session = await service.GetSessionById(sessionId);
            return Ok(session);
        }

        // UPDATE Existing

        // End Current Session
        [HttpPut, Route("{sessionId}/End")]
        [Authorize(Roles = "Admin, Chair")]
        public async Task<IHttpActionResult> EndCurrentSession(int sessionId)
        {
            var service = GetSessionService();
            if (await service.EndSession(sessionId))
                return Ok("Session ended successfully.");

            return BadRequest("Cannot end session.");
        }

        // DELETE Existing by ID
        [HttpDelete, Route("{sessionId}/Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteSessionById(int sessionId)
        {
            var service = GetSessionService();
            if (await service.DeleteSessionById(sessionId))
                return Ok($"Session {sessionId} deleted successfully.");

            return BadRequest("Cannot delete session.");
        }

        private SessionService GetSessionService() => new SessionService(User.Identity.GetUserId());
    }
}
