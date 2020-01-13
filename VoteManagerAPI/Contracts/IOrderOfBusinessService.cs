using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models.OOB;
using VoteManagerAPI.Models.Vote;

namespace VoteManagerAPI.Contracts
{
    public interface IOrderOfBusinessService : IAuthorizedService
    {
        // GET Tabled OOBs
        Task<TableDetail> GetTableAsync();

        // GET Votes By ID
        Task<List<VoteDetail>> GetVotesAsync(int orderOfBusinessId);

        // DELETE By ID
        Task<bool> DeleteByIdAsync(int orderOfBusinessId);

        // Toggle Tabled OOB
        Task<bool> ToggleTabledByIdAsync(int orderOfBusinessId);

        // Conclude OOB
        Task<bool> ConcludeOrderAsync(int orderOfBusinessId);
    }
}