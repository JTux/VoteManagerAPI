using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoteManagerAPI.Models;

namespace VoteManagerAPI.Services
{
    public class OrderOfBusinessService
    {
        private readonly string _userId;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public OrderOfBusinessService() { }
        public OrderOfBusinessService(string userId) => _userId = userId;

        // GET Votes By ID

        // DELETE By ID

        // Table OOB

        // Conclude OOB

        // Get Tabled OOB
    }
}