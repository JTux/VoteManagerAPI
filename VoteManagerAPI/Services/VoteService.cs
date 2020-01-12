using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Extensions;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Entities;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Services
{
    public class VoteService
    {
        private readonly string _userId;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public VoteService() { }
        public VoteService(string userId) => _userId = userId;

        // CAST Vote
        public async Task<bool> CastVoteAsync(VoteCreate model)
        {
            var targetOrder = await _context.OrdersOfBusiness.FindAsync(model.OrderOfBusinessId);
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

            return await _context.SaveChangesAsync() == 1;
        }

        // GET User's Vote
        public async Task<VoteDetail> GetUsersVoteAsync(int orderId)
        {
            if (string.IsNullOrEmpty(_userId))
                return null;

            var entity = await _context.Votes.FirstOrDefaultAsync(v => v.VoterId == _userId && v.OrderOfBusinessId == orderId);

            return entity.ToDetail();
        }
    }
}