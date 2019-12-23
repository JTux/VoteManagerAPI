using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Entities;
using VoteManagerAPI.Models.Motion;

namespace VoteManagerAPI.Services
{
    public class MotionService
    {
        private readonly string _userId;

        public MotionService() { }
        public MotionService(string userId) => _userId = userId;

        public async Task<bool> CreateMotion(MotionCreate model)
        {
            using (var context = new ApplicationDbContext())
            {
                var currentSession = await context.Sessions.FirstOrDefaultAsync(s => s.IsActive);
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

                return await context.SaveChangesAsync() == 1;
            }
        }

        // GET MOTION

        // Update Existing Motion

        // Table Motion

        // Conclude Motion

        // Get Tabled Motions
    }
}