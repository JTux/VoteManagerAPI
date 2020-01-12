using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Extensions;
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
        
        // CREATE New
        public async Task<bool> CreateAmendment(AmendmentCreate model)
        {
            using (var context = new ApplicationDbContext())
            {
                var currentSessionId = await new SessionService(_userId).GetCurrentSessionId();
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

                context.Amendments.Add(amendment);
                return await context.SaveChangesAsync() == 1;
            }
        }

        // GET By ID
        public async Task<AmendmentDetail> GetAmendmentById(int amendmentId)
        {
            using (var context = new ApplicationDbContext())
            {
                var entity = await context.OrdersOfBusiness.FindAsync(amendmentId);
                if (entity == null || entity is MotionEntity)
                    return null;

                var amendmentDetail = (AmendmentDetail)entity.ToDetail();
                return amendmentDetail;
            }
        }

        // GET All Motions

        // Update Existing
    }
}