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
    public class MotionController : ApiController
    {
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

        private MotionService GetMotionService() => new MotionService(User.Identity.GetUserId());
    }
}
