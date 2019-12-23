using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Entities;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Services
{
    public class VoteService
    {
        private readonly string _userId;

        public VoteService() { }
        public VoteService(string userId) => _userId = userId;

        public async Task<bool> CastVote(VoteCreate model)
        {
            using (var context = new ApplicationDbContext())
            {
                var targetOrder = await context.OrdersOfBusiness.FindAsync(model.OrderOfBusinessId);
                if (targetOrder == null)
                    return false;

                var userVote = targetOrder.Votes.FirstOrDefault(o => o.VoterId == _userId);

                if (userVote != null)
                {
                    userVote.Status = (userVote.Status != model.Status) ? model.Status : VoteStatus.NoVote;
                    userVote.IsAnonymous = model.IsAnonymous;
                }
                else
                {
                    targetOrder.Votes.Add(new VoteEntity { Status = model.Status, VoterId = _userId, IsAnonymous = model.IsAnonymous });
                }

                return await context.SaveChangesAsync() == 1;
            }
        }
    }
}