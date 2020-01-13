using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Contracts
{
    public interface IVoteService : IAuthorizedService
    {
        // CAST Vote
        Task<bool> CastVoteAsync(VoteCreate model);

        // GET User's Vote
        Task<VoteDetail> GetUsersVoteAsync(int orderId);
    }
}