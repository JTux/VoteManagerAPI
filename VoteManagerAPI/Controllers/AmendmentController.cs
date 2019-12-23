using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VoteManagerAPI.Models.Amendment;
using VoteManagerAPI.Services;

namespace VoteManagerAPI.Controllers
{
    [RoutePrefix("api/Amendment")]
    public class AmendmentController : ApiController
    {
        [HttpPost, Route("Present")]
        [Authorize(Roles = "Admin, Chair, Founder, Member")]
        public async Task<IHttpActionResult> PresentNewAmendment(AmendmentCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            var service = GetAmendmentService();
            if (await service.CreateAmendment(model))
                return Ok("Amendment presented successfully.");

            return BadRequest("Cannot create amendment.");
        }

        [HttpGet, Route("{amendmentId}")]
        public async Task<IHttpActionResult> GetAmendmentById(int amendmentId)
        {
            var service = new AmendmentService();
            var amendment = await service.GetAmendmentById(amendmentId);
            return Ok(amendment);
        }

        private AmendmentService GetAmendmentService() => new AmendmentService(User.Identity.GetUserId());
    }
}
