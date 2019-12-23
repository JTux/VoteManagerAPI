using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Entities;

namespace VoteManagerAPI.Services
{
    public class SessionService
    {
        private string _userId;

        public SessionService() { }
        public SessionService(string userId) => _userId = userId;

        public bool StartNewSession()
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Sessions.Where(s => s.IsActive).Count() != 0)
                    return false;

                context.Sessions.Add(new SessionEntity { CreatorId = _userId, StartDate = DateTime.UtcNow, IsActive = true });

                return context.SaveChanges() == 1;
            }
        }

        // GET CURRENT SESSION

        public bool EndCurrentSession()
        {
            using (var context = new ApplicationDbContext())
            {
                int changeCount = 1;

                var currentSession = context.Sessions.FirstOrDefault(s => s.IsActive);
                if (currentSession == null)
                    return false;

                currentSession.IsActive = false;

                foreach (var order in context.OrdersOfBusiness.Where(o => o.IsActive))
                {
                    order.IsTabled = true;
                    order.IsActive = false;
                    changeCount++;
                }

                return context.SaveChanges() == changeCount;
            }
        }
    }
}