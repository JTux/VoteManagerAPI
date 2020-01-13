using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using VoteManagerAPI.Models.Amendment;

namespace VoteManagerAPI.Contracts
{
    public interface IAmendmentService : IAuthorizedService
    {
        // CREATE New
        Task<bool> CreateAmendmentAsync(AmendmentCreate model);

        // GET By ID
        Task<AmendmentDetail> GetAmendmentByIdAsync(int amendmentId);

        // GET All Amendments
        Task<List<AmendmentDetail>> GetAllMotionsAsync();

        // Update Existing
        Task<bool> UpdateExistingAmendmentAsync(AmendmentUpdate model);
    }
}