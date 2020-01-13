using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VoteManagerAPI.Contracts;
using VoteManagerAPI.Models.Motion;
using VoteManagerAPI.Services;

namespace VoteManagerAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Motion")]
    public class MotionController : ApiController
    {
        private readonly IMotionService _service;

        public MotionController(IMotionService service)
        {
            _service = service;
        }

        // CREATE New
        [HttpPost, Route("Present")]
        [Authorize(Roles = "Admin, Chair, Founder, Member")]
        public async Task<IHttpActionResult> PresentNewMotion(MotionCreate model)
        {
            _service.SetUserId(User.Identity.GetUserId());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            if (await _service.CreateMotionAsync(model))
                return Ok("Motion created.");

            return BadRequest("Cannot present motion.");
        }

        // GET By ID
        [HttpGet, Route("{motionId}")]
        public async Task<IHttpActionResult> GetMotionById(int motionId)
        {
            _service.SetUserId(User.Identity.GetUserId());

            if (motionId < 1)
                return BadRequest($"Motion ID cannot be less than 1. Targeted ID was {motionId}.");

            var detailModel = await _service.GetMotionByIdAsync(motionId);
            if (detailModel != null)
                return Ok(detailModel);

            return BadRequest($"No Motion found with ID of {motionId}.");
        }

        // GET All Motions
        [HttpGet, Route("All")]
        public async Task<IHttpActionResult> GetAllMotions()
        {
            _service.SetUserId(User.Identity.GetUserId());

            return Ok(await _service.GetAllMotionsAsync());
        }

        // UPDATE Existing
        [HttpPut, Route("{motionId}/Update")]
        [Authorize(Roles = "Admin, Chair, Founder, Member")]
        public async Task<IHttpActionResult> UpdateExistingMotion(int motionId, MotionUpdate model)
        {
            _service.SetUserId(User.Identity.GetUserId());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (model == null)
                return BadRequest("Request body cannot be empty.");

            if (motionId != model.MotionId)
                return BadRequest($"Body ID ({model.MotionId}) and URI ID ({motionId}) mismatch.");

            if (await _service.UpdateExistingMotionAsync(model))
                return Ok("Motion updated.");

            return BadRequest("Cannot update motion.");
        }
    }
}
