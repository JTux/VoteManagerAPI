using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Entities;

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

        // GET CURRENT SESSION

        // GET CURRENT SESSION ID

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