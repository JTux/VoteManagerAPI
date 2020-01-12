using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Extensions;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Amendment;
using VoteManagerAPI.Models.Rule;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Services
{
    public class RuleService
    {
        private readonly string _userId;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public RuleService() { }
        public RuleService(string userId) => _userId = userId;

        // GET All Rules
        public async Task<List<RuleDetail>> GetAllRulesAsync()
        {
            return (await _context.Rules.ToListAsync()).Select(r => r.ToDetail()).ToList();
        }

        // GET Rule By ID
        public async Task<RuleDetail> GetRuleByIdAsync(int ruleId)
        {
            var rule = await _context.Rules.FindAsync(ruleId);
            return rule?.ToDetail();
        }

        // DELETE Existing
        public async Task<bool> DeleteRuleAsync(int ruleId)
        {
            var rule = await _context.Rules.FindAsync(ruleId);
            if (rule == null)
                return false;

            _context.Rules.Remove(rule);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}