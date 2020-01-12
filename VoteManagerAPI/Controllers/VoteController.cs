using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VoteManagerAPI.Models.Vote;
using VoteManagerAPI.Services;

namespace VoteManagerAPI.Controllers
{
    [Authorize(Roles = "Admin, Chair, Founder, Member")]
    [RoutePrefix("api/Vote")]
    public class VoteController : ApiController
    {
        [HttpPut, Route("Cast")]
        public async Task<IHttpActionResult> CastVote(VoteCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            var service = GetVoteService();
            if (await service.CastVoteAsync(model))
                return Ok("Vote cast.");

            return BadRequest("Vote cannot be submitted.");
        }

        private VoteService GetVoteService() => new VoteService(User.Identity.GetUserId());
    }
}
