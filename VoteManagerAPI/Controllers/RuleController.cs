using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VoteManagerAPI.Contracts;
using VoteManagerAPI.Models.Rule;
using VoteManagerAPI.Services;

namespace VoteManagerAPI.Controllers
{
    [RoutePrefix("api/Rule")]
    public class RuleController : ApiController
    {
        private readonly IRuleService _service;

        public RuleController(IRuleService service)
        {
            _service = service;
        }

        // GET All Rules
        [HttpGet, Route("All")]
        public async Task<IHttpActionResult> GetAllRules()
        {
            var rules = await _service.GetAllRulesAsync();
            return Ok(rules);
        }

        // GET Rule By ID
        [HttpGet, Route("{ruleId}")]
        public async Task<IHttpActionResult> GetRuleById(int ruleId)
        {
            var rule = await _service.GetRuleByIdAsync(ruleId);
            return Ok(rule);
        }

        // DELETE Existing
        [HttpDelete, Route("{ruleId:int}/Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteExistingRule(int ruleId)
        {
            _service.SetUserId(User.Identity.GetUserId());

            if (await _service.DeleteRuleAsync(ruleId))
                return Ok("Rule deleted successfully.");

            return BadRequest("Could not delete rule.");
        }
    }
}
