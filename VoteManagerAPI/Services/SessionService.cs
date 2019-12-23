using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Amendment;
using VoteManagerAPI.Models.Entities;
using VoteManagerAPI.Models.Motion;
using VoteManagerAPI.Models.Session;

namespace VoteManagerAPI.Services
{
    public class SessionService
    {
        private readonly string _userId;

        public SessionService() { }
        public SessionService(string userId) => _userId = userId;

        public async Task<bool> StartNewSession()
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Sessions.Where(s => s.IsActive).Count() != 0)
                    return false;

                context.Sessions.Add(new SessionEntity { CreatorId = _userId, StartDate = DateTime.UtcNow, IsActive = true });

                return await context.SaveChangesAsync() == 1;
            }
        }

        // GET SESSION STATUS => Is it safe to end?

        public async Task<SessionDetail> GetSessionById(int sessionId)
        {
            using (var context = new ApplicationDbContext())
            {
                var session = await context.Sessions.FindAsync(sessionId);
                if (session == null)
                    return null;

                var sessionDetail = new SessionDetail
                {
                    SessionId = session.Id,
                    StartDate = session.StartDate,
                    CreatorName = session.Creator.UserName,
                    IsActive = session.IsActive,
                    Motions = new List<MotionDetail>(),
                    Amendments = new List<AmendmentDetail>()
                };

                var motionService = new MotionService(_userId);
                var amendmentService = new AmendmentService(_userId);

                foreach (var motion in session.OrdersOfBusiness.Where(o => o is MotionEntity))
                    sessionDetail.Motions.Add(await motionService.GetMotionById(motion.Id));

                foreach (var amendment in session.OrdersOfBusiness.Where(o => o is AmendmentEntity))
                    sessionDetail.Amendments.Add(await amendmentService.GetAmendmentById(amendment.Id));

                return sessionDetail;
            }
        }

        public async Task<int> GetCurrentSessionId()
        {
            using(var context = new ApplicationDbContext())
            {
                var currentSession = await context.Sessions.FirstOrDefaultAsync(s => s.IsActive);
                return currentSession.Id;
            }
        }

        public async Task<bool> EndCurrentSession()
        {
            using (var context = new ApplicationDbContext())
            {
                int changeCount = 1;

                var currentSession = await context.Sessions.FirstOrDefaultAsync(s => s.IsActive);
                if (currentSession == null)
                    return false;

                currentSession.IsActive = false;

                foreach (var order in context.OrdersOfBusiness.Where(o => o.IsActive))
                {
                    order.IsTabled = true;
                    order.IsActive = false;
                    changeCount++;
                }

                return await context.SaveChangesAsync() == changeCount;
            }
        }
    }
}