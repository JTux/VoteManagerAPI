using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VoteManagerAPI.Contracts;
using VoteManagerAPI.Models.Vote;
using VoteManagerAPI.Services;

namespace VoteManagerAPI.Controllers
{
    [Authorize(Roles = "Admin, Chair, Founder, Member")]
    public class VoteController : ApiController
    {
        private readonly IVoteService _service;

        public VoteController(IVoteService service)
        {
            _service = service;
        }

        // CAST Vote
        [HttpPut, Route("~/api/Motion/{id:int}/CastVote"), Route("~/api/Amendment/{id:int}/CastVote")]
        public async Task<IHttpActionResult> CastVote(int id, VoteCreate model)
        {
            _service.SetUserId(User.Identity.GetUserId());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            if (model.OrderOfBusinessId != id)
                return BadRequest($"Body ID ({model.OrderOfBusinessId}) and URI ID ({id}) mismatch.");

            if (await _service.CastVoteAsync(model))
                return Ok("Vote cast.");

            return BadRequest("Vote cannot be submitted.");
        }

        // GET User's Vote
        [HttpGet, Route("~/api/Motion/{id:int}/UserVote"), Route("~/api/Amendment/{id:int}/UserVote")]
        public async Task<IHttpActionResult> GetUserVote(int id)
        {
            _service.SetUserId(User.Identity.GetUserId());
            return Ok(await _service.GetUsersVoteAsync(id));
        }
    }
}
