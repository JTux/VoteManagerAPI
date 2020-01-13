using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Contracts;
using VoteManagerAPI.Extensions;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Amendment;
using VoteManagerAPI.Models.Entities;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Services
{
    public class AmendmentService : IAmendmentService
    {
        private string _userId;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public AmendmentService() { }

        public void SetUserId(string userId)
        {
            _userId = userId;
        }

        // CREATE New
        public async Task<bool> CreateAmendmentAsync(AmendmentCreate model)
        {
            var sessionService = new SessionService();
            sessionService.SetUserId(_userId);

            var currentSessionId = await sessionService.GetCurrentSessionIdAsync();
            if (currentSessionId == 0)
                return false;

            var amendment = new AmendmentEntity
            {
                RuleId = model.RuleId,
                Title = model.Title,
                Description = model.Description,
                IsActive = true,
                PresentingUserId = _userId,
                OriginalSessionId = currentSessionId,
            };

            _context.Amendments.Add(amendment);
            return await _context.SaveChangesAsync() == 1;
        }

        // GET By ID
        public async Task<AmendmentDetail> GetAmendmentByIdAsync(int amendmentId)
        {
            var entity = await _context.OrdersOfBusiness.FindAsync(amendmentId);
            if (entity == null || entity is MotionEntity)
                return null;

            var amendmentDetail = (AmendmentDetail)entity.ToDetail();
            return amendmentDetail;
        }

        // GET All Amendments
        public async Task<List<AmendmentDetail>> GetAllMotionsAsync()
        {
            return (await _context.Amendments.ToListAsync()).Select(m => m.ToDetail()).ToList();
        }

        // Update Existing
        public async Task<bool> UpdateExistingAmendmentAsync(AmendmentUpdate model)
        {
            var motion = await _context.Amendments.FindAsync(model.AmendmentId);
            if (motion == null || motion.PresentingUserId != _userId)
                return false;

            if (motion.Title == model.Title && motion.Description == model.Description)
                return false;

            int changeCount = 0;

            for (int totalVotes = motion.Votes.Count; changeCount < totalVotes; changeCount++)
                _context.Votes.Remove(motion.Votes.ElementAt(0));

            motion.Title = model.Title;
            motion.Description = model.Description;

            return await _context.SaveChangesAsync() == changeCount + 1;
        }
    }
}