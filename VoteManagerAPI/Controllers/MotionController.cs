using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VoteManagerAPI.Models.Motion;
using VoteManagerAPI.Services;

namespace VoteManagerAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Motion")]
    public class MotionController : ApiController
    {
        // CREATE New
        [HttpPost, Route("Present")]
        [Authorize(Roles = "Admin, Chair, Founder, Member")]
        public async Task<IHttpActionResult> PresentNewMotion(MotionCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            var service = GetMotionService();
            if (await service.CreateMotion(model))
                return Ok("Motion created.");

            return BadRequest("Cannot present motion.");
        }

        // GET By ID
        [HttpGet, Route("{motionId}")]
        public async Task<IHttpActionResult> GetMotionById(int motionId)
        {
            if (motionId < 1)
                return BadRequest($"Motion ID cannot be less than 1. Targeted ID was {motionId}.");

            var service = GetMotionService();

            var detailModel = await service.GetMotionById(motionId);
            if (detailModel != null)
                return Ok(detailModel);

            return BadRequest($"No Motion found with ID of {motionId}.");
        }

        // GET Votes By Motion ID

        // GET All Motions

        // UPDATE Existing
        [HttpPut, Route("{motionId}/Update")]
        [Authorize(Roles = "Admin, Chair, Founder, Member")]
        public async Task<IHttpActionResult> UpdateExistingMotion(int motionId, MotionUpdate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            if (motionId != model.MotionId)
                return BadRequest($"Body ID ({model.MotionId}) and URI ID ({motionId}) mismatch.");

            var service = GetMotionService();

            if (await service.UpdateExistingMotion(model))
                return Ok("Motion updated.");

            return BadRequest("Cannot update motion.");
        }

        // DELETE By ID

        // Table Motion

        // Conclude Motion

        // Get Tabled Motion

        private MotionService GetMotionService() => new MotionService(User.Identity.GetUserId());
    }
}
