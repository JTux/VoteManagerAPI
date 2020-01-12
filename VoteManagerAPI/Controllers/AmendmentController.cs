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
        // CREATE New
        [HttpPost, Route("Present")]
        [Authorize(Roles = "Admin, Chair, Founder, Member")]
        public async Task<IHttpActionResult> PresentNewAmendment(AmendmentCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            var service = GetAmendmentService();
            if (await service.CreateAmendmentAsync(model))
                return Ok("Amendment presented successfully.");

            return BadRequest("Cannot create amendment.");
        }

        // GET By ID
        [HttpGet, Route("{amendmentId}")]
        public async Task<IHttpActionResult> GetAmendmentById(int amendmentId)
        {
            var service = new AmendmentService();
            var amendment = await service.GetAmendmentByIdAsync(amendmentId);
            return Ok(amendment);
        }

        // GET Votes By Amendment ID

        // GET All Amendments

        // UPDATE Existing

        // DELETE Existing

        // Table Amendment

        // Conclude Amendment

        // Get Tabled Amendment

        private AmendmentService GetAmendmentService() => new AmendmentService(User.Identity.GetUserId());
    }
}
