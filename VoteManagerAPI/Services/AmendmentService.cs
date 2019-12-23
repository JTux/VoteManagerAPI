using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Amendment;
using VoteManagerAPI.Models.Entities;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Services
{
    public class AmendmentService
    {
        private readonly string _userId;

        public AmendmentService() { }
        public AmendmentService(string userId) => _userId = userId;

        public async Task<AmendmentDetail> GetAmendmentById(int amendmentId)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = await context.OrdersOfBusiness.FindAsync(amendmentId);
                if (entity == null || entity is MotionEntity)
                    return null;

                var amendment = (AmendmentEntity)entity;

                var amendmentDetail = new AmendmentDetail
                {
                    AmendmentId = amendment.Id,
                    RuleId = amendment.RuleId,
                    Title = amendment.Title,
                    Description = amendment.Description,
                    IsPassed = amendment.IsPassed,
                    IsActive = amendment.IsActive,
                    IsTabled = amendment.IsTabled,
                    PresenterName = amendment.PresentingUser.UserName,
                    Votes = new List<VoteDetail>()
                };

                foreach (var vote in amendment.Votes)
                {
                    var voteDetail = new VoteDetail
                    {
                        VoteId = vote.Id,
                        OrderOfBusinessId = vote.OrderOfBusinessId,
                        Status = vote.Status,
                        VoterName = (vote.IsAnonymous) ? "Anonymous" : vote.Voter.UserName
                    };

                    amendmentDetail.Votes.Add(voteDetail);
                }

                return amendmentDetail;
            }
        }
    }
}