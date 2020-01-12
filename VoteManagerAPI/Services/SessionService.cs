using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Extensions;
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
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public SessionService() { }
        public SessionService(string userId) => _userId = userId;

        // CREATE
        public async Task<bool> StartNewSessionAsync()
        {
            if (await GetCurrentSessionIdAsync() == 0)
                _context.Sessions.Add(new SessionEntity { CreatorId = _userId, StartDate = DateTime.UtcNow, IsActive = true });

            return await _context.SaveChangesAsync() == 1;
        }

        // GET Current
        public async Task<int> GetCurrentSessionIdAsync()
        {
            var currentSession = await _context.Sessions.FirstOrDefaultAsync(s => s.IsActive);
            return currentSession != null ? currentSession.Id : 0;
        }

        // GET Session List
        public async Task<List<SessionListItem>> GetSessionListAsync()
        {
            var allSessions = (await _context.Sessions.ToListAsync()).Select(s => s.ToListItem());
            return allSessions.OrderByDescending(s => s.StartDate).ToList();
        }

        // GET All
        public async Task<List<SessionDetail>> GetAllSessionsAsync()
        {
            var allSessions = (await _context.Sessions.ToListAsync()).Select(s => s.ToDetail());
            return allSessions.OrderByDescending(s => s.StartDate).ToList();
        }

        // GET by ID
        public async Task<SessionDetail> GetSessionByIdAsync(int sessionId)
        {
            var session = await _context.Sessions.FindAsync(sessionId);
            return session?.ToDetail();
        }

        // UPDATE Existing

        // End Current Session
        public async Task<bool> EndSessionAsync(int sessionId)
        {
            int changeCount = 1;

            var currentSession = await _context.Sessions.FindAsync(sessionId);
            if (currentSession == null)
                return false;

            currentSession.IsActive = false;

            foreach (var order in _context.OrdersOfBusiness.Where(o => o.IsActive))
            {
                order.IsTabled = true;
                order.IsActive = false;
                changeCount++;
            }

            return await _context.SaveChangesAsync() == changeCount;
        }

        // DELETE Existing by ID
        public async Task<bool> DeleteSessionByIdAsync(int sessionId)
        {
            var entity = await _context.Sessions.FindAsync(sessionId);
            if (entity == null)
                return false;

            _context.Sessions.Remove(entity);
            return await _context.SaveChangesAsync() == 1;
        }

        // GET SESSION STATUS => Is it safe to end?
    }
}