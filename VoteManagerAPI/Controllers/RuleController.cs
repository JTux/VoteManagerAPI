using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VoteManagerAPI.Models.Rule;
using VoteManagerAPI.Services;

namespace VoteManagerAPI.Controllers
{
    [RoutePrefix("api/Rule")]
    public class RuleController : ApiController
    {
        // GET All Rules
        [HttpGet, Route("All")]
        public async Task<IHttpActionResult> GetAllRules()
        {
            var service = new RuleService();
            var rules = await service.GetAllRulesAsync();
            return Ok(rules);
        }

        // GET Rule By ID
        [HttpGet, Route("{ruleId}")]
        public async Task<IHttpActionResult> GetRuleById(int ruleId)
        {
            var service = new RuleService();
            var rule = await service.GetRuleByIdAsync(ruleId);
            return Ok(rule);
        }

        // DELETE Existing
        [HttpDelete, Route("{ruleId:int}/Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteExistingRule(int ruleId)
        {
            var service = GetRuleService();
            if (await service.DeleteRuleAsync(ruleId))
                return Ok("Rule deleted successfully.");

            return BadRequest("Could not delete rule.");
        }

        private RuleService GetRuleService() => new RuleService(User.Identity.GetUserId());
    }
}
