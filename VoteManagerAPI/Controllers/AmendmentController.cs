using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VoteManagerAPI.Services;

namespace VoteManagerAPI.Controllers
{
    [RoutePrefix("api/Amendment")]
    public class AmendmentController : ApiController
    {
        [Route("{amendmentId}")]
        public async Task<IHttpActionResult> GetAmendmentById(int amendmentId)
        {
            var service = new AmendmentService();
            var amendment = await service.GetAmendmentById(amendmentId);
            return Ok(amendment);
        }

        private AmendmentService GetAmendmentService() => new AmendmentService(User.Identity.GetUserId());
    }
}
