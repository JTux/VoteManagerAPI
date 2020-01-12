﻿using System;
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
        public async Task<bool> StartNewSession()
        {
            if (await GetCurrentSessionId() == 0)
                _context.Sessions.Add(new SessionEntity { CreatorId = _userId, StartDate = DateTime.UtcNow, IsActive = true });

            return await _context.SaveChangesAsync() == 1;
        }

        // GET Current
        public async Task<int> GetCurrentSessionId()
        {
            var currentSession = await _context.Sessions.FirstOrDefaultAsync(s => s.IsActive);
            return currentSession != null ? currentSession.Id : 0;
        }

        // GET All
        public async Task<List<SessionDetail>> GetAllSessions()
        {
            var allSessions = (await _context.Sessions.ToListAsync()).Select(s => s.ToDetail());
            var orderedSessions = allSessions.OrderByDescending(s => s.StartDate).ToList();
            return orderedSessions;
        }

        // GET by ID
        public async Task<SessionDetail> GetSessionById(int sessionId)
        {
            var session = await _context.Sessions.FindAsync(sessionId);
            return session?.ToDetail();
        }

        // UPDATE Existing

        // End Current Session
        public async Task<bool> EndSession(int sessionId)
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
        public async Task<bool> DeleteSessionById(int sessionId)
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