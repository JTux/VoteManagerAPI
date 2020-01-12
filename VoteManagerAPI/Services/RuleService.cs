using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Amendment;
using VoteManagerAPI.Models.Rule;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Services
{
    public class RuleService
    {
        private readonly string _userId;

        public RuleService() { }
        public RuleService(string userId) => _userId = userId;

        public async Task<List<RuleDetail>> GetAllRulesAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                var rules = await context.Rules.ToListAsync();

                var allRules = new List<RuleDetail>();

                foreach (var rule in rules)
                    allRules.Add(await GetRuleByIdAsync(rule.Id));

                return allRules;
            }
        }

        public async Task<RuleDetail> GetRuleByIdAsync(int ruleId)
        {
            using (var context = new ApplicationDbContext())
            {
                var rule = await context.Rules.FindAsync(ruleId);
                if (rule == null)
                    return null;

                var detail = new RuleDetail
                {
                    RuleId = rule.Id,
                    MotionId = rule.OriginalOrderId,
                    MotionTitle = rule.OrderOfBusiness.Title,
                    DatePassed = rule.DatePassed,
                    Amendments = rule.Amendments.Where(a => a.IsPassed).Select(a => new AmendmentDetail
                    {
                        AmendmentId = a.Id,
                        RuleId = rule.Id,
                        Title = a.Title,
                        Description = a.Description,
                        IsPassed = a.IsPassed,
                        IsActive = a.IsActive,
                        IsTabled = a.IsTabled,
                        PresenterName = a.PresentingUser.UserName,
                        Votes = a.Votes.Select(vote => new VoteDetail
                        {
                            VoteId = vote.Id,
                            OrderOfBusinessId = vote.OrderOfBusinessId,
                            Status = vote.Status,
                            VoterName = (vote.IsAnonymous) ? "Anonymous" : vote.Voter.UserName
                        }).ToList()
                    }).ToList()
                };

                return detail;
            }
        }
    }
}