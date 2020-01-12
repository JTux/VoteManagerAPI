using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Extensions;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Entities;
using VoteManagerAPI.Models.Motion;
using VoteManagerAPI.Models.OOB;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Services
{
    public class MotionService
    {
        private readonly string _userId;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public MotionService() { }
        public MotionService(string userId) => _userId = userId;

        public async Task<bool> CreateMotion(MotionCreate model)
        {
            var currentSession = await _context.Sessions.FirstOrDefaultAsync(s => s.IsActive);
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

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<OrderOfBusinessDetail> GetMotionById(int motionId)
        {
            var motion = await _context.OrdersOfBusiness.FindAsync(motionId);
            if (motion == null || motion is AmendmentEntity)
                return null;

            var motionDetail = motion.ToDetail();
            return motionDetail;
        }

        public async Task<bool> UpdateExistingMotion(MotionUpdate model)
        {
            var motion = await _context.OrdersOfBusiness.FindAsync(model.MotionId);
            if (motion == null || motion is AmendmentEntity || motion.PresentingUserId != _userId)
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

        // Table Motion

        // Conclude Motion

        // Get Tabled Motions
    }
}