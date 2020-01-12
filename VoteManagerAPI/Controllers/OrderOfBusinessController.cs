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
    public class OrderOfBusinessController : ApiController
    {
        // GET Tabled OOBs
        [HttpGet, Route("api/Table")]
        public async Task<IHttpActionResult> GetTable() => Ok(await GetService().GetTableAsync());

        // GET Votes By ID
        [HttpGet, Route("api/Motion/{id:int}/Votes"), Route("api/Amendment/{id:int}/Votes")]
        [Authorize]
        public async Task<IHttpActionResult> GetVotes(int id)
        {
            var service = GetService();
            return Ok(await service.GetVotesAsync(id));
        }

        // DELETE By ID
        [HttpDelete, Route("api/Motion/{id:int}/Delete"), Route("api/Amendment/{id:int}/Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteById(int id)
        {
            var service = GetService();
            if (await service.DeleteByIdAsync(id))
                return Ok($"Order of Business {id} has been deleted successfully.");

            return BadRequest($"Could not delete Order of Business {id}.");
        }

        // Toggle Tabled OOB
        [HttpPut, Route("api/Motion/{id:int}/Toggle"), Route("api/Amendment/{id:int}/Toggle")]
        [Authorize(Roles = "Admin, Chair")]
        public async Task<IHttpActionResult> ToggleTabledStatus(int id)
        {
            var service = GetService();
            if (await service.ToggleTabledByIdAsync(id))
                return Ok($"Order of Business {id} table status toggled successfully.");

            return BadRequest($"Could not toggle table status for Order of Business {id}.");
        }

        // Conclude OOB
        [HttpPut, Route("api/Motion/{id:int}/Conclude"), Route("api/Amendment/{id:int}/Conclude")]
        [Authorize(Roles = "Admin, Chair")]
        public async Task<IHttpActionResult> ConcludeOrderOfBusiness(int id)
        {
            var service = GetService();
            if (await service.ConcludeOrderAsync(id))
                return Ok($"Order of Business {id} concluded successfully.");

            return BadRequest($"Could not conclude Order of Business {id}.");
        }

        private OrderOfBusinessService GetService()
        {
            return new OrderOfBusinessService(User.Identity.GetUserId());
        }
    }
}
