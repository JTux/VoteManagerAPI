using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Extensions;
using VoteManagerAPI.Models;
using VoteManagerAPI.Models.Entities;
using VoteManagerAPI.Models.OOB;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Services
{
    public class OrderOfBusinessService
    {
        private readonly string _userId;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public OrderOfBusinessService() { }
        public OrderOfBusinessService(string userId) => _userId = userId;

        // GET Votes By ID
        public async Task<List<VoteDetail>> GetVotesAsync(int orderOfBusinessId)
        {
            var orderOfBusiness = await _context.OrdersOfBusiness.FindAsync(orderOfBusinessId);
            if (orderOfBusiness == null)
                return null;

            return orderOfBusiness.Votes.Select(v => v.ToDetail()).ToList();
        }

        // DELETE By ID
        public async Task<bool> DeleteByIdAsync(int orderOfBusinessId)
        {
            var orderOfBusiness = await _context.OrdersOfBusiness.FindAsync(orderOfBusinessId);
            if (orderOfBusiness == null)
                return false;

            _context.OrdersOfBusiness.Remove(orderOfBusiness);
            return await _context.SaveChangesAsync() == 1;
        }

        // Table OOB
        public async Task<bool> ToggleTabledByIdAsync(int orderOfBusinessId)
        {
            var orderOfBusiness = await _context.OrdersOfBusiness.FindAsync(orderOfBusinessId);
            if (orderOfBusiness == null || (!orderOfBusiness.IsTabled && !orderOfBusiness.IsActive))
                return false;

            orderOfBusiness.IsTabled = !orderOfBusiness.IsTabled;
            orderOfBusiness.IsActive = !orderOfBusiness.IsActive;

            return await _context.SaveChangesAsync() == 2;
        }

        // Conclude OOB
        public async Task<bool> ConcludeOrderAsync(int orderOfBusinessId)
        {
            var orderOfBusiness = await _context.OrdersOfBusiness.FindAsync(orderOfBusinessId);
            if (orderOfBusiness == null || orderOfBusiness.IsTabled || !orderOfBusiness.IsActive)
                return false;

            int ayes = 0, nays = 0;
            foreach (var vote in orderOfBusiness.Votes)
            {
                switch (vote.Status)
                {
                    case VoteStatus.Aye:
                        ayes++;
                        break;
                    case VoteStatus.Nay:
                        nays++;
                        break;
                }
            }

            orderOfBusiness.IsActive = false;

            if (ayes > nays)
            {
                if (!(orderOfBusiness is AmendmentEntity))
                {
                    var newRule = new RuleEntity { DatePassed = DateTime.UtcNow, OriginalOrderId = orderOfBusiness.Id };
                    _context.Rules.Add(newRule);
                }
                else
                {
                    var amendment = (AmendmentEntity)orderOfBusiness;
                    amendment.IsPassed = true;
                }

                return await _context.SaveChangesAsync() == 2;
            }

            return await _context.SaveChangesAsync() == 1;
        }
    }
}