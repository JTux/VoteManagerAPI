using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models.Rule;

namespace VoteManagerAPI.Contracts
{
    public interface IRuleService : IAuthorizedService
    {
        // GET All Rules
        Task<List<RuleDetail>> GetAllRulesAsync();

        // GET Rule By ID
        Task<RuleDetail> GetRuleByIdAsync(int ruleId);

        // DELETE Existing
        Task<bool> DeleteRuleAsync(int ruleId);
    }
}