using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult InitializeSession()
        {
            var service = GetSessionService();
            if (service.StartNewSession())
                return Ok("Started new session.");

            return BadRequest("Session already in progress.");
        }

        // GET CURRENT SESSION

        [HttpPut, Route("End")]
        [Authorize(Roles = "Admin, Chair")]
        public IHttpActionResult EndCurrentSession()
        {
            var service = GetSessionService();
            if (service.EndCurrentSession())
                return Ok("Session ended successfully.");

            return BadRequest("Cannot end session.");
        }

        private SessionService GetSessionService() => new SessionService(User.Identity.GetUserId());
    }
}
