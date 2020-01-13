using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VoteManagerAPI.Contracts;
using VoteManagerAPI.Services;

namespace VoteManagerAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Session")]
    public class SessionController : ApiController
    {
        private readonly ISessionService _service;

        public SessionController(ISessionService service)
        {
            _service = service;
        }

        // CREATE
        [HttpPost, Route("Begin")]
        [Authorize(Roles = "Admin, Chair")]
        public async Task<IHttpActionResult> InitializeSession()
        {
            _service.SetUserId(User.Identity.GetUserId());

            if (await _service.StartNewSessionAsync())
                return Ok("Started new session.");

            return BadRequest("Session already in progress.");
        }

        // GET Current
        [HttpGet, Route("Current")]
        public async Task<IHttpActionResult> GetCurrentSession()
        {
            _service.SetUserId(User.Identity.GetUserId());

            var currentSessionId = await _service.GetCurrentSessionIdAsync();
            if (currentSessionId != 0)
            {
                var currentSession = await _service.GetSessionByIdAsync(currentSessionId);
                return Ok(currentSession);
            }

            return BadRequest("There is no active session.");
        }

        // GET Session List
        [HttpGet, Route("Index")]
        public async Task<IHttpActionResult> GetSessionList() => Ok(await _service.GetSessionListAsync());

        // GET All
        [HttpGet, Route("All")]
        public async Task<IHttpActionResult> GetAllSessions() => Ok(await _service.GetAllSessionsAsync());

        // GET by ID
        [HttpGet, Route("{sessionId:int}")]
        public async Task<IHttpActionResult> GetSessionById(int sessionId)
        {
            var session = await _service.GetSessionByIdAsync(sessionId);
            return Ok(session);
        }

        // UPDATE Existing

        // End Current Session
        [HttpPut, Route("{sessionId}/End")]
        [Authorize(Roles = "Admin, Chair")]
        public async Task<IHttpActionResult> EndCurrentSession(int sessionId)
        {
            _service.SetUserId(User.Identity.GetUserId());

            if (await _service.EndSessionAsync(sessionId))
                return Ok("Session ended successfully.");

            return BadRequest("Cannot end session.");
        }

        // DELETE Existing by ID
        [HttpDelete, Route("{sessionId}/Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteSessionById(int sessionId)
        {
            _service.SetUserId(User.Identity.GetUserId());

            if (await _service.DeleteSessionByIdAsync(sessionId))
                return Ok($"Session {sessionId} deleted successfully.");

            return BadRequest("Cannot delete session.");
        }
    }
}
