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
    [Authorize]
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
            var service = GetAmendmentService();
            var amendment = await service.GetAmendmentByIdAsync(amendmentId);
            return Ok(amendment);
        }

        // GET All Amendments
        [HttpGet, Route("All")]
        public async Task<IHttpActionResult> GetAllAmendements()
        {
            var service = GetAmendmentService();
            return Ok(await service.GetAllMotionsAsync());
        }

        // UPDATE Existing
        [HttpPut, Route("{motionId}/Update")]
        [Authorize(Roles = "Admin, Chair, Founder, Member")]
        public async Task<IHttpActionResult> UpdateExistingMotion(int motionId, AmendmentUpdate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            if (motionId != model.AmendmentId)
                return BadRequest($"Body ID ({model.AmendmentId}) and URI ID ({motionId}) mismatch.");

            var service = GetAmendmentService();

            if (await service.UpdateExistingAmendmentAsync(model))
                return Ok("Amendment updated.");

            return BadRequest("Cannot update amendment.");
        }

        private AmendmentService GetAmendmentService() => new AmendmentService(User.Identity.GetUserId());
    }
}
