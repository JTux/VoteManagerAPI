using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Entities;
using VoteManagerAPI.Models.Motion;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Services
{
    public class MotionService
    {
        private readonly string _userId;

        public MotionService() { }
        public MotionService(string userId) => _userId = userId;

        public async Task<bool> CreateMotion(MotionCreate model)
        {
            using (var context = new ApplicationDbContext())
            {
                var currentSession = await context.Sessions.FirstOrDefaultAsync(s => s.IsActive);
                if (currentSession == null)
                    return false;

                var newMotion = new MotionEntity
                {
                    Title = model.Title,
                    Description = model.Description,
                    IsActive = true,
                    IsTabled = false,
                    PresentingUserId = _userId
                };

                currentSession.OrdersOfBusiness.Add(newMotion);

                return await context.SaveChangesAsync() == 1;
            }
        }

        public async Task<MotionDetail> GetMotionById(int motionId)
        {
            using (var context = new ApplicationDbContext())
            {
                var motion = await context.OrdersOfBusiness.FindAsync(motionId);
                if (motion == null || motion is AmendmentEntity)
                    return null;

                var motionDetail = new MotionDetail
                {
                    MotionId = motion.Id,
                    Title = motion.Title,
                    Description = motion.Description,
                    IsActive = motion.IsActive,
                    IsTabled = motion.IsTabled,
                    PresenterName = motion.PresentingUser.UserName,
                    Votes = new List<VoteDetail>()
                };

                foreach (var vote in motion.Votes)
                {
                    var voteDetail = new VoteDetail
                    {
                        VoteId = vote.Id,
                        OrderOfBusinessId = vote.OrderOfBusinessId,
                        Status = vote.Status,
                        VoterName = (vote.IsAnonymous) ? "Anonymous" : vote.Voter.UserName
                    };

                    motionDetail.Votes.Add(voteDetail);
                }

                return motionDetail;
            }
        }

        public async Task<bool> UpdateExistingMotion(MotionUpdate model)
        {
            using (var context = new ApplicationDbContext())
            {
                var motion = await context.OrdersOfBusiness.FindAsync(model.MotionId);
                if (motion == null || motion is AmendmentEntity || motion.PresentingUserId != _userId)
                    return false;

                if (motion.Title == model.Title && motion.Description == model.Description)
                    return false;

                int changeCount = 0;

                for (int totalVotes = motion.Votes.Count; changeCount < totalVotes; changeCount++)
                {
                    context.Votes.Remove(motion.Votes.ElementAt(0));
                }

                motion.Title = model.Title;
                motion.Description = model.Description;

                return await context.SaveChangesAsync() == changeCount + 1;
            }
        }

        // Table Motion

        // Conclude Motion

        // Get Tabled Motions
    }
}